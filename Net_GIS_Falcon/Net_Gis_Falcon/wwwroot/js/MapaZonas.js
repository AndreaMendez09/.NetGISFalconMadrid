var baseMapLayer = new ol.layer.Tile({
    source: new ol.source.OSM(),
});

var features = new ol.source.Vector({ wrapX: false });

features.on('addfeature', function (evt) {
    var feature = evt.feature;
    //var array = new Array();
    var coords = feature.getGeometry().getCoordinates();
    //array = feature.getGeometry().getCoordinates();
    //var coords = array.join(':');
    //var coords1 = coords[1];
    var coordinates = "";

    for (var i = 0; i < coords.length; i++) {
        for (var j = 0; j < coords[i].length; j++) {
            coordinates += coords[i][j].toString() + ": ";
        }
    }

    document.getElementById('info').value = coordinates;
    /*console.log(coords[0][0]);
    console.log(coords);
    console.log(coordinates);*/
});

var vector = new ol.layer.Vector({
    source: features,
});
var countriesLayer = new ol.layer.Vector({
    source: new ol.source.Vector({
        url: './countries.json',
        format: new ol.format.GeoJSON(),
    }),
    style: function (feature) {
        style.getText().setText(feature.get('name'));
        return style;
    },
});

var map = new ol.Map({
    layers: [baseMapLayer, vector, countriesLayer],
    target: 'map',
    view: new ol.View({
        projection: 'EPSG:4326',
        center: [0, 0],
        zoom: 5,
    })
});

var typeSelect = document.getElementById('type');

var draw; // global so we can remove it later
function addInteraction() {
    var value = typeSelect.value;
    if (value !== 'None') {
        var geometryFunction;
        if (value === 'Square') {
            value = 'Circle';
            geometryFunction = ol.interaction.Draw.createRegularPolygon(4);
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
            source: features,
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
    //draw.removeLastPoint();
        features.clear();  // implicit remove of last feature
});



addInteraction();