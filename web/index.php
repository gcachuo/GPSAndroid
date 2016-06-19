<?php
/**
 * Created by PhpStorm.
 * User: Memo
 * Date: 03/jun/2016
 * Time: 02:32 PM
 */
session_start();
?>
<html>
<head>
    <title>El Merodeador</title>
    <?php include "scripts.php" ?>
</head>
<body>
<h3>El Merodeador</h3>

<?php

if (!isset($_SESSION["usuario"])) {
    include "login.php";
} else {
    $_SESSION["mod"] = isset($_POST["mod"]) ? $_POST["mod"] : $_SESSION["mod"];
    switch ($_SESSION["mod"]) {
        case "mapa":
            include "mapa.php";
            break;
        default:
            include "login.php";
            break;
    }
}
?>
</body>
</html>
