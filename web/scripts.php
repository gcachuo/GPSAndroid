<link rel="stylesheet" href="css/bootstrap.css">
<link rel="stylesheet" href="css/styles.css">
<script src="js/jquery-2.1.4.js"></script>
<script src="js/bootstrap.js"></script>

<script>
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
    function cargarCoordenadas(id) {
        $.post(
            "ajax.php",
            {
                ajaxAccion: "cargarCoordenadas",
                id: id
            },
            function (out) {

            }
        );
    }
</script>