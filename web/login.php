<?php
/**
 * Created by PhpStorm.
 * User: Memo
 * Date: 03/jun/2016
 * Time: 03:19 PM
 */
if(!isset($_SESSION)){
    header('Location: index.php');
}
?>
<form id="frmLogin" action="index.php" method="post">
    <input type="hidden" value="mapa" name="mod">
</form>
<div class="form-horizontal">
    <div class="row">
        <label for="txtUser" class="col-md-1 col-md-offset-4 control-label">Usuario:</label>
        <div class="col-md-3">
            <input id="txtUser" name="usuario" type="text" class="form-control">
        </div>
    </div>
    <div class="row">
        <label for="txtpass" class="col-md-1 col-md-offset-4 control-label">Password:</label>
        <div class="col-md-3">
            <input id="txtPass" name="password" type="password" class="form-control">
        </div>
    </div>
    <div class="row">
        <div class="col-md-2 col-md-offset-5">
            <a id="btnIngresar" class="btn btn-primary col-md-12" onclick="ingresar()">Ingresar</a>
        </div>
        <div class="col-md-12" style="text-align: center">
            <label id="lblEstatus"></label>
        </div>
    </div>
</div>
