<?php
/**
 * Created by PhpStorm.
 * User: Memo
 * Date: 03/jun/2016
 * Time: 03:15 PM
 */

session_start();
require "bd.php";
$bd = new bd;

if (isset($_POST["ajaxAccion"])) {
    switch ($_POST["ajaxAccion"]) {
        case "ingresar":
            echo ingresar();
            break;
        case "logout":
            logout();
            break;
        case "cargarCoordenadas":
            echo cargarCoordenadas();
            break;
        case "cargarUsuarios":
            echo cargarUsuarios();
            break;
        case "focusMarker":
            $_SESSION["marker"] = $_POST["id"];
            break;
    }
}

function inicio()
{
    global $bd;

    $consulta = $bd->consulta("SELECT * FROM usuario;");
    $siguiente = $bd->siguiente($consulta);
    echo $siguiente["nombre_usuario"];
}

function ingresar()
{
    global $bd;
    $user = $_POST["user"];
    $pass = $_POST["pass"];

    if ($user == "" || $pass == "") {
        return "Llene todos los campos";
    }

    $sql = $bd->consulta("select id_usuario id from usuario where nombre_usuario='$user' and pass_usuario='$pass'");
    $consulta = $bd->siguiente($sql);
    if ($consulta == null) {
        return "Error al ingresar";
    }
    if ($sql->num_rows > 0) {
        $_SESSION["usuario"] = $consulta["id"];
        return "true";
    }

    return "Datos Incorrectos";
}

function logout()
{
    session_destroy();
}

function cargarCoordenadas()
{
    global $bd;
    $id = isset($_SESSION["marker"]) ? $_SESSION["marker"] : $_POST["id"];
    $coord = array();

    $consulta = $bd->consulta("SELECT id_usuario id, latitud_coordenada lat, longitud_coordenada lng FROM coordenada ORDER BY FIELD(id_usuario, $id) ASC ");

    foreach ($consulta as $reg) {
        $coord['id' . $reg["id"]]["nombre"] = $bd->siguiente($bd->consulta("SELECT nombre_usuario nombre FROM usuario WHERE id_usuario=" . $reg["id"]))["nombre"];
        $coord['id' . $reg["id"]]["lat"] = $reg["lat"] * 1;
        $coord['id' . $reg["id"]]["lng"] = $reg["lng"] * 1;
    }

    return json_encode($coord);
}

function cargarUsuarios()
{

    global $bd;
    $usuarios = "";
    $consulta = $bd->consulta("SELECT * FROM usuario");
    foreach ($consulta as $reg) {
        $usuarios .= '<a class="btn btn-default" onclick="focusMarker(' . $reg["id_usuario"] . ')">' . $reg["nombre_usuario"] . '</a> ';
    }

    return $usuarios;
}

?>