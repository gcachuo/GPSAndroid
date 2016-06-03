<?php

/**
 * Created by PhpStorm.
 * User: Memo
 * Date: 03/jun/2016
 * Time: 02:37 PM
 */
class bd
{
    protected $host;
    protected $user;
    protected $pass;
    protected $bd;
    protected $conexion;

    public function __construct()
    {
        $this->host = "localhost";
        $this->user = "root";
        $this->pass = "sqlserver";
        $this->bd = "gpsandroid";
        $this->conexion = $this->conectar();

    }

    function conectar()
    {
        $mysqli = new mysqli($this->host, $this->user, $this->pass, $this->bd);
        if ($mysqli->connect_errno) {
            return "Fallo al conectar a MySQL: (" . $mysqli->connect_errno . ") " . $mysqli->connect_error;
        }
        return $mysqli;
    }

    function consulta($sql)
    {
        $resultado = $this->conexion->query($sql);
        return $resultado;
    }

    function siguiente($consulta)
    {
        if(!$consulta){
         return null;
        }
        else {
            $resultado = $consulta->fetch_assoc();
            return $resultado;
        }
    }
}

?>