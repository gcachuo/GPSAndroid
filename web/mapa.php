<?php
/**
 * Created by PhpStorm.
 * User: Memo
 * Date: 03/jun/2016
 * Time: 04:45 PM
 */
if(!isset($_SESSION)){
    header('Location: index.php');
}
?>
<form id="frmMapa" action="index.php" method="post">
    <a class="btn btn-default" onclick="logout()">Cerrar Sesión</a>
</form>
Hola mapa
