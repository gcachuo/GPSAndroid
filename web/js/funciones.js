/**
 * Created by Memo on 04/jun/2016.
 */
function ingresar() {
    var user = $("#txtUser").val();
    var pass = $("#txtPass").val();

    $("#lblEstatus").html("");
    $("#btnIngresar").attr("disabled", true);
    $.post(
        "ajax.php",
        {
            ajaxAccion: "ingresar",
            user: user,
            pass: pass
        },
        function (out) {
            if (out != "true") {
                $("#lblEstatus").html(out);
            }
            else {
                $("#frmLogin").submit();
            }
            $("#btnIngresar").attr("disabled", false);
        });
}
function logout() {
    $.post(
        "ajax.php",
        {
            ajaxAccion: "logout"
        },
        function (out) {
            $("#frmMapa").submit();
        }
    )
}
var markers = [];
var map;
function cargarCoordenadas(id) {
    setMapOnAll(null);
    $.post(
        "ajax.php",
        {
            ajaxAccion: "cargarCoordenadas",
            id: id
        },
        function (out) {
            var obj = JSON.parse(out);

            $.each(obj,function (index,value) {
                var myLatLng = {lat: value.lat, lng: value.lng};
                setMarker(value.nombre, myLatLng);
            });

        }
    )
}
function setMapOnAll(map) {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}
function initMap(label, lat, lng) {
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 17
    });
}
function setMarker(label, myLatLng) {
    var marker = new google.maps.Marker({
        position: myLatLng,
        title: label
    });
    markers.push(marker);
    marker.setMap(map);
    map.setCenter(marker.getPosition());
}
function cargarUsuarios(){
    $.post(
        "ajax.php",
        {
            ajaxAccion: "cargarUsuarios"
        },
        function (out) {
            $("#users").html(out);
        }
    )
}
function focusMarker(id) {
    $.post(
        "ajax.php",
        {
            ajaxAccion: "focusMarker",
            id:id
        },
        function (out) {
            cargarCoordenadas(id);
        }
    )
}