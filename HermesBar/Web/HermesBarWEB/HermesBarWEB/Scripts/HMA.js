var data = new Date();
var mile_atual = data.getTime();
VerifyConnection();
function VerifyConnection() {
    jQuery.ajaxSetup({ async: false });
    re = "";
    r = Math.round(Math.random() * 10000);
    $.get("http://1.bp.blogspot.com/-LtDtdVE1roA/UmAavs_T_iI/AAAAAAAADNY/g0L-HAPlkTY/s1600/0060.png", { subins: r }, function (d) {
        re = true;
    }).error(function () {
        re = false;
        alert('Você está sem conexão com a internet, seus dados serão salvos localmente');
    });
    return re;
}