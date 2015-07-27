/*******************************GLOBAL VARIABLES**************************************/
var data = new Date();
var mile_atual = data.getTime();
var resultRequest = '';
/***************************END GLOBAL VARIABLES**************************************/

/*************************************MASK********************************************/
$('#Cnpj').mask('00.000.000/0000-00');
$('#Cep').mask('00000-000');
$('#Telefone').mask('(00)0000-00009')
$('#Celular').mask('(00)0000-00009');
/*********************************END MASK********************************************/

/********************************LAYOUT METHODS**************************************/
var VerifyEmail = setInterval(function () { VerifyEmails() }, 300000);
VerifyConnection();
function VerifyEmails() {
    $.ajax({
        type: 'GET',
        url: '/Home/GetEmails',
        data: null,
        async: true,
        cache: false,
        success: function (data) {
            if (data != null) {
                $('#Quantidade-Email').html(data.EmailCount);
                $('#Quantidade-Email-Aviso').html('Você possui ' + data.EmailCount + ' novo e-mail');
                GenerateEmailInfo(data);
            }
        },
        statusCode: {
            404: function (content) { console.log('cannot find resource'); },
            500: function (content) { console.log('internal server error'); }
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        }
    });
}
function GenerateEmailInfo(data) {
    for (var i = 0; i < data.List.length; i++) {
        if (data.List[i].Subject.length > 40) {
            data.List[i].Subject = data.List[i].Subject.substr(0, 40);
        }
        $('#Infos-Email').append('<a href="#">' + '<span class="photo"></span>' + '<span class="subject"><span class="from" id="From">' + data.List[i].From + '</span><span class="time">1 min</span></span><span id="Subject">' + data.List[i].Subject +'</span></a>');
    }
}
/****************************END LAYOUT METHODS**************************************/

/******************************SUPPLIER METHODS**************************************/
$('body').on('focusout', '#Cnpj', function () {
    if (!CnpjValidade($(this).val())) {
        $('.cnpj-error').css('color', 'red');
        $(this).css('border-color', 'red');
        $('.cnpj-error').html('Insira um CNPJ válido');
    } else {
        $('.cnpj-error').css('color', '');
        $(this).css('border-color', '');
        $('.cnpj-error').html('');
    }
});
$('body').on('focusout', '#InscricaoEstadual', function () {
    if ($(this).val() == '' || $(this).val() == undefined)
        $(this).val('ISENTO');
});
$('body').on('focusout', '#InscricaoMunicipal', function () {
    if ($(this).val() == '' || $(this).val() == undefined)
        $(this).val('ISENTO');
});
/**************************END SUPPLIER METHODS**************************************/

/***********************************AUX METHODS**************************************/
function CnpjValidade(cnpj){
    cnpj = cnpj.replace(/[^\d]+/g, '');

    if (cnpj == '') return false;

    if (cnpj.length != 14)
        return false;

    if (cnpj == "00000000000000" ||
        cnpj == "11111111111111" ||
        cnpj == "22222222222222" ||
        cnpj == "33333333333333" ||
        cnpj == "44444444444444" ||
        cnpj == "55555555555555" ||
        cnpj == "66666666666666" ||
        cnpj == "77777777777777" ||
        cnpj == "88888888888888" ||
        cnpj == "99999999999999")
        return false;

    tamanho = cnpj.length - 2
    numeros = cnpj.substring(0, tamanho);
    digitos = cnpj.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0))
        return false;

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(1))
        return false;

    return true;
}
function GenerateRequest(type, url, data, async) {
    $.ajax({
        type: type,
        url: url,
        data: data,
        async: async,
        cache: false,
        success: function (data) {
            if (resultRequest != null) {
                resultRequest = null;
            }
            resultRequest = data;
        },
        statusCode: {
            404: function (content) { console.log('cannot find resource'); },
            500: function (content) { console.log('internal server error'); }
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        }
    });
}
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
/********************************END AUX METHODS**************************************/