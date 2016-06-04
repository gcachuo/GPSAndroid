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
    $id=$_POST["id"];
    $coord = array();

    $consulta = $bd->siguiente($bd->consulta("SELECT latitud_coordenada lat, longitud_coordenada lng FROM coordenada where id_usuario=$id ORDER BY hora_coordenada DESC "));
    $coord["nombre"] = $bd->siguiente($bd->consulta("SELECT nombre_usuario nombre FROM usuario"))["nombre"];
    $coord["lat"] = $consulta["lat"] * 1;
    $coord["lng"] = $consulta["lng"] * 1;

    return json_encode($coord);
}

?>