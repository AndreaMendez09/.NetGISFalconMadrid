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

var layerSource;

if (document.getElementById('idUser') != null) {
    /* (GeoServer Query)
        SELECT peticion.id_peticion,
            peticion.fecha_creacion,
            peticion.localizacion_peticion,
            peticion.precision_peticion,
            peticion.usuario,
            st_geomfromtext(st_astext(peticion.localizacion_peticion:: geometry), 4326) AS localizacion
        FROM peticion WHERE peticion.usuario = %idUsuario%
        */


    var idUser = document.getElementById('idUser').innerHTML;
    console.log(idUser);
    layerSource = new ol.source.ImageWMS({
        url: 'http://localhost:8080/geoserver/wms?viewparams=idUsuario:' + idUser,
        params: {
            'LAYERS': 'peticionZonas'
            //'LAYERS': 'view_peticion'
        },
        serverType: 'geoserver',
    });

    var layerF = new ol.source.ImageWMS({
        url: 'http://localhost:8080/geoserver/wms?viewparams=idOperador:' + idUser,
        params: {
            'LAYERS': 'peticionZonas'
        },
        serverType: 'geoserver',
    });
} else {
    layerSource = new ol.source.ImageWMS({
        url: 'http://localhost:8080/geoserver/wms',
        params: {
            'LAYERS': 'peticionZonas'
            //'LAYERS': 'view_peticion'
        },
        serverType: 'geoserver',
    });

    var layerF = new ol.source.ImageWMS({
        url: 'http://localhost:8080/geoserver/wms',
        params: {
            'LAYERS': 'peticionZonas'
        },
        serverType: 'geoserver',
    });
}

if (document.getElementById('rolUser') != null) {
    if (document.getElementById('rolUser').innerHTML == "operador") {
        layerSource = new ol.source.ImageWMS({
            url: 'http://localhost:8080/geoserver/wms&viewparams=idOperador:' + document.getElementById('idUser').innerHTML,
            params: {
                'LAYERS': 'peticionZonas'
                //'LAYERS': 'view_peticion'
            },
            serverType: 'geoserver',
        });

        var layerF = new ol.source.ImageWMS({
            url: 'http://localhost:8080/geoserver/wms',
            params: {
                'LAYERS': 'view_peticion'
            },
            serverType: 'geoserver',
        });
    }
}

var layerP = new ol.layer.Image({
    title: 'punto',
    source: layerSource,
    visible: true
});

var layerFF = new ol.layer.Image({
    title: 'perimetro',
    source: layerF,
    visible: true,
    className: 'perimetro'
});

const baseLayerGroup = new ol.layer.Group({
    //layers: [baseMapLayer, mapQuestOSMLayer, countriesLayer]
    layers: [baseMapLayer, mapQuestOSMLayer, countriesLayer, layerFF, layerP]
});

var map = new ol.Map({
    target: 'map',
    view: new ol.View({
        projection: 'EPSG:4326',
        center: [-3, 40],
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
            if (baseLayerTitle != "perimetro" && baseLayerTitle != "punto") {
                element.setVisible(baseLayerTitle === baseLayerElementValue)
                console.log(element.get('title'));
            }
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

