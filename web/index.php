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
    <title>GPS Android</title>
</head>
<body>
<h3>GPS Android</h3>

<?php

if(!isset($_SESSION["usuario"])) {
    include "login.php";
}
else{
    switch ($_POST["mod"]){
        case "mapa":
            include "mapa.php";
            break;
    }
}
?>
<?php include "scripts.php" ?>
</body>
</html>
