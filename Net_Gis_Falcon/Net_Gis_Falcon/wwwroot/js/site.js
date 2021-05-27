var baseMapLayer = new ol.layer.Tile({
    source: new ol.source.OSM(),
    title: 'OSMBase'
});

var features = new ol.source.Vector({ wrapX: false });

var vector = new ol.layer.Vector({
    source: features,
});
var countriesLayer = new ol.layer.Vector({
    source: new ol.source.Vector({
        //url: './georef-spain-municipio.json',    //Municipios (Demasiado grande, tarda mucho en cargar, no lo recomiendo)
        url: './countries.json',                 //Países
        //url: './provincias-espanolas.json',        //Provincias
        format: new ol.format.GeoJSON(),
    }),
    style: function (feature) {
        //style.getText().setText(feature.get('acom_name'));   //Municipios
        style.getText().setText(feature.get('name'));        //Países
        //style.getText().setText(feature.get('provincia'));   //Provincias
        return style;
    },
    title: 'OSMCountries',
    visible: false
});

var mapQuestOSMLayer = new ol.layer.Tile({
    source: new ol.source.OSM({
        url: 'http://{a-c}.tile.openstreetmap.fr/hot/{z}/{x}/{y}.png'
    }),
    visible: false,
    title: 'OSMHumanitarian'
});

const baseLayerGroup = new ol.layer.Group({
    layers: [baseMapLayer, mapQuestOSMLayer, countriesLayer]
});

var map = new ol.Map({
    target: 'map',
    view: new ol.View({
        center: [-412270.1806743107, 4926716.659837265],
        zoom: 5,
    }),
});

map.addLayer(baseLayerGroup)

const baseLayerElements = document.querySelectorAll(".sidebar > input[type=radio]");

for (let baseLayerElement of baseLayerElements) {
    baseLayerElement.addEventListener('change', function () {
        let baseLayerElementValue = this.value;
        baseLayerGroup.getLayers().forEach(function (element, index, array) {
            let baseLayerTitle = element.get('title');
            element.setVisible(baseLayerTitle === baseLayerElementValue)
            console.log(element.get('title'));
        })
    })
}


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
        //highlightStyle.getText().setText(feature.get('acom_name'));   //Municipios
        highlightStyle.getText().setText(feature.get('name'));        //Países
        //highlightStyle.getText().setText(feature.get('provincia'));     //´Provincias
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
        //info.innerHTML = feature.get('acom_name');   //Municipios
        info.innerHTML = feature.get('name');        //Países
        //info.innerHTML = feature.get('provincia');     //Provincias
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

