<?php
/**
 * Created by PhpStorm.
 * User: Memo
 * Date: 03/jun/2016
 * Time: 04:45 PM
 */
if (!isset($_SESSION)) {
    header('Location: index.php');
}
?>
<form id="frmMapa" action="index.php" method="post" class="container">
    <div class="row">
        <a class="btn btn-default" onclick="logout()">Cerrar Sesión</a>
    </div>
    <div id="users" class="row"></div>
</form>
<script>
    var segundos = 10;

    $(function () {
        cargarUsuarios();
        initMap();
        cargarCoordenadas(<?php echo $_SESSION["usuario"]?>)
        setInterval(function () {
            cargarCoordenadas(<?php echo $_SESSION["usuario"]?>)
        }, segundos * 1000);
    });
</script>
<div id="map"></div>
