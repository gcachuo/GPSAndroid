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
function logout(){
    session_destroy();
}

?>