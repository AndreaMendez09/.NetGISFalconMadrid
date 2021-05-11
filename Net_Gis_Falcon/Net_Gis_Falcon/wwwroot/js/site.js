var raster = new ol.layer.Tile({
    source: new ol.source.OSM(),
});

var source = new ol.source.Vector({ wrapX: false });

source.on('addfeature', function (evt) {
    var feature = evt.feature;
    var coords = feature.getGeometry().getCoordinates();

    console.log(coords);
});

var vector = new ol.layer.Vector({
    source: source,
});
var countryLayer = new ol.layer.Vector({
    source: new ol.source.Vector({
        url: './countries.geojson',
        format: new ol.format.GeoJSON(),
    }),
    style: function (feature) {
        style.getText().setText(feature.get('name'));
        return style;
    },
});

var map = new ol.Map({
    layers: [raster, vector, countryLayer],
    target: 'map',
    view: new ol.View({
        center: [-412270.1806743107, 4926716.659837265],
        zoom: 5,
    }),
});

var style = new ol.style.Style({
    fill: new ol.style.Fill({
        color: 'rgba(255, 255, 255, 0.6)',
    }),
    stroke: new ol.style.Stroke({
        color: '#319FD3',
        width: 1,
    }),
    text: new ol.style.Text({
        font: '12px Calibri,sans-serif',
        fill: new ol.style.Fill({
            color: '#000',
        }),
        stroke: new ol.style.Stroke({
            color: '#fff',
            width: 3,
        }),
    }),
});

var highlightStyle = new ol.style.Style({
    stroke: new ol.style.Stroke({
        color: '#f00',
        width: 1,
    }),
    fill: new ol.style.Fill({
        color: 'rgba(255,0,0,0.1)',
    }),
    text: new ol.style.Text({
        font: '12px Calibri,sans-serif',
        fill: new ol.style.Fill({
            color: '#000',
        }),
        stroke: new ol.style.Stroke({
            color: '#f00',
            width: 3,
        }),
    }),
});

var featureOverlay = new ol.layer.Vector({
    source: new ol.source.Vector(),
    map: map,
    style: function (feature) {
        highlightStyle.getText().setText(feature.get('name'));
        return highlightStyle;
    },
});

var highlight;
var displayFeatureInfo = function (pixel) {
    var feature = map.forEachFeatureAtPixel(pixel, function (feature) {
        return feature;
    });

    var info = document.getElementById('info');
    if (feature) {
        info.innerHTML = feature.getId() + ': ' + feature.get('name');
    } else {
        info.innerHTML = '&nbsp;';
    }

    if (feature !== highlight) {
        if (highlight) {
            featureOverlay.getSource().removeFeature(highlight);
        }
        if (feature) {
            featureOverlay.getSource().addFeature(feature);
        }
        highlight = feature;
    }
};

map.on('pointermove', function (evt) {
    if (evt.dragging) {
        return;
    }
    var pixel = map.getEventPixel(evt.originalEvent);
    displayFeatureInfo(pixel);
});
map.on('click', function (evt) {
    displayFeatureInfo(evt.pixel);
});

var typeSelect = document.getElementById('type');

var draw; // global so we can remove it later
var geometryFunction;
function addInteraction() {
    var value = typeSelect.value;
    if (value !== 'None') {
        if (value === 'Square') {
            value = 'Circle';
            geometryFunction = ol.interaction.Draw.createRegularPolygon(4);
        } else if (value === 'Point') {
            value = 'Point';
            geometryFunction = null;
        } else if (value === 'Box') {
            value = 'Circle';
            geometryFunction = ol.interaction.Draw.createBox();
        } else if (value === 'Star') {
            value = 'Circle';
            geometryFunction = function (coordinates, geometry) {
                var center = coordinates[0];
                var last = coordinates[coordinates.length - 1];
                var dx = center[0] - last[0];
                var dy = center[1] - last[1];
                var radius = Math.sqrt(dx * dx + dy * dy);
                var rotation = Math.atan2(dy, dx);
                var newCoordinates = [];
                var numPoints = 12;
                for (var i = 0; i < numPoints; ++i) {
                    var angle = rotation + (i * 2 * Math.PI) / numPoints;
                    var fraction = i % 2 === 0 ? 1 : 0.5;
                    var offsetX = radius * fraction * Math.cos(angle);
                    var offsetY = radius * fraction * Math.sin(angle);
                    newCoordinates.push([center[0] + offsetX, center[1] + offsetY]);
                }
                newCoordinates.push(newCoordinates[0].slice());
                if (!geometry) {
                    geometry = new ol.geom.Polygon([newCoordinates]);
                } else {
                    geometry.setCoordinates([newCoordinates]);
                }

                return geometry;
            };
        }

        draw = new ol.interaction.Draw({
            source: source,
            type: value,
            geometryFunction: geometryFunction,
        });
        map.addInteraction(draw);
    }
}

/**
 * Handle change event.
 */
typeSelect.onchange = function () {
    map.removeInteraction(draw);
    addInteraction();
};

document.getElementById('undo').addEventListener('click', function () {
    draw.removeLastPoint();
});

addInteraction();