/*******************************GLOBAL VARIABLES**************************************/
var data = new Date();
var mile_atual = data.getTime();
var resultRequest = '';
var evento = '';

/***************************END GLOBAL VARIABLES**************************************/

/*************************************MASK********************************************/
$('#Cnpj').mask('00.000.000/0000-00');
$('#Cpf').mask('000.000.000-00');
$('#Cep').mask('00000-000');
$('#Telefone').mask('(00)0000-00009')
$('#Celular').mask('(00)0000-00009');
$('#telefone-cliente').mask('(00)0000-00009');
$('#ValorCompra').mask('000.000.000.000.000,00', { reverse: true });
$('#ValorVenda').mask('000.000.000.000.000,00', { reverse: true });
$('#caixa-valor-inicial').mask('000.000.000.000.000,00', { reverse: true });
$('#reforco-valor-caixa').mask('000.000.000.000.000,00', { reverse: true });
$('#sangria-valor-caixa').mask('000.000.000.000.000,00', { reverse: true });
$('#caixa-valor-final').mask('000.000.000.000.000,00', { reverse: true });
$('#nascimento-cliente').mask('00/00/0000');
$('#data-abertura-caixa').val(FormatActualDate(new Date()));
$('#data-fechamento-caixa').val(FormatActualDate(new Date()));
$('#data-entrada-cliente').val(FormatActualDate(new Date()));
$('#data-reforco-caixa').val(FormatActualDate(new Date()));
$('#data-sangria-caixa').val(FormatActualDate(new Date()));
$('#codigo-cliente').focus();
/*********************************END MASK********************************************/

/********************************LAYOUT METHODS**************************************/
var VerifyEmail = setInterval(function () { VerifyEmails() }, 300000);
var VerifyProduct = setInterval(function () { VerifyProducts() }, 300000);
var connection = VerifyConnection();
//if (connection != false) {
//    initDatabase();
//}
$('body').on('click', '#tela-cheia', function () {
    alert('criar método para deixar em tela cheia');
});
function VerifyEmails() {
    $.ajax({
        type: 'GET',
        url: '/Home/GetEmails',
        data: null,
        async: true,
        cache: false,
        success: function (data) {
            if (data != null) {
                GenerateTimeMessage('Atenção', 'Você possui novos e-mails', 'success');
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
function VerifyProducts() {
    $.ajax({
        type: 'GET',
        url: '/Produto/GetProdutosBaixo',
        data: null,
        async: true,
        cache: false,
        success: function (data) {
            if (data != null) {
                GenerateTimeMessage('Atenção', 'Produtos em baixa no estoque', 'warning');
                GenerateProductInfo(data);
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
function GenerateProductInfo(data) {
    $('#quant-produto-baixa').append(data.length);
    $('#nao-produto-baixa').remove();
    for (var i = 0; i < data.length; i++) {
        $('#produtos-baixa').append('<a href="/Produto/GetId?Id='+data[i].Id+'"><span class="label label-primary"><i class="icon_profile"></i></span>' + 'Produto: ' + data[i].Nome + ' |  Quantidade: ' + data[i].QuantidadeAtual + '</a>')
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

/*******************************PRODUCT METHODS**************************************/
$('body').on('click', '#novo-tipo', function () {
    swal({
        title: "Cadastro rápido!",
        text: "Insira o tipo:",
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-top",
        inputPlaceholder: "Insira o nome"
    }, function (inputValue) {
        if (inputValue === false)
            return false;
        if (inputValue === "") {
            swal.showInputError("O campo está em branco :(");
            return false
        }
        var data = { nome : inputValue };
        GenerateRequest('GET', '/Tipo/CadastroRapido', data, false);
        if (resultRequest == 'True') {
            swal("Legal!", "Novo tipo cadastrado!", "success");
            GenerateRequest('GET', '/Tipo/GetJson', null, false);
            if (resultRequest != null) {
                GenerateTypeList(resultRequest);
            }
        } else {
            swal("Oops!", "Ocorreu um erro ao gravar o novo tipo de produto!", "error");
        }
    });
});

$('body').on('click', '#novo-unidade', function () {
    swal({
        title: "Cadastro rápido!",
        text: "Insira a Unidade:",
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-top",
        inputPlaceholder: "Insira a Unidade"
    }, function (inputValue) {
        if (inputValue === false)
            return false;
        if (inputValue === "") {
            swal.showInputError("O campo está em branco :(");
            return false
        }
        var data = { nome: inputValue };
        GenerateRequest('GET', '/UnidadeMedida/CadastroRapido', data, false);
        if (resultRequest == 'True') {
            swal("Legal!", "Nova Unidade cadastrada!", "success");
            GenerateRequest('GET', '/UnidadeMedida/GetJson', null, false);
            if (resultRequest != null) {
                GenerateUnityList(resultRequest);
            }
        } else {
            swal("Oops!", "Ocorreu um erro ao gravar a nova unidade de produto!", "error");
        }
    });
});

function GenerateTypeList(data) {
    $('#TipoSelected option').remove();
    var selecione = '<option value="0">Selecione</option>'
    $('#TipoSelected').append(selecione);
    for (var i = 0; i < data.length; i++) {
        $('#TipoSelected').append('<option value="'+data[i].Id+'">'+data[i].Nome+'</option>')
    }
}

function GenerateUnityList(data) {
    $('#UnidadeMedidaSelected option').remove();
    var selecione = '<option value="0">Selecione</option>'
    $('#UnidadeMedidaSelected').append(selecione);
    for (var i = 0; i < data.length; i++) {
        $('#UnidadeMedidaSelected').append('<option value="' + data[i].Id + '">' + data[i].Nome + '</option>')
    }
}
/****************************END PRODUCT METHODS*************************************/

/*********************************CLIENT METHODS*************************************/
$('body').on('focusout', '#DataNascimento', function () {
    var nascimento = new Date($(this).val());
    var hoje = new Date();
    if (Math.floor(Math.ceil(Math.abs(nascimento.getTime() - hoje.getTime()) / (1000 * 3600 * 24)) / 365.25) > 18)
    {
        $(this).css('border-color', '');
        return;
    }
    else
    {
        GenerateMessage('Oops!', 'Idade inferior a 18 anos!', 'warning');
        $(this).css('border-color', 'red');
    }     
});
/*****************************END CLIENT METHODS*************************************/
/******************************CALENDAR METHODS**************************************/
$('#calendar').fullCalendar({
    lang: 'pt-br',
    header: {
        left: 'prev,next today',
        center: 'title',
        right: 'month,agendaWeek,agendaDay'  
    },
    editable: true,
    eventLimit: true,
    events: GetValues(),
    eventClick: function (calEvent, jsEvent, view) {
        var data = { id: calEvent.id };
        window.location = '/Agenda/Editar?id=' + calEvent.id;       
    },
    eventAfterRender: function (event, element, view) {
    var dataHoje = new Date();
        if (event.start < dataHoje && event.end > dataHoje) {
            //event.color = "#FFB347"; //Em andamento
            element.css('background-color', '#FFB347');
        } else if (event.start < dataHoje && event.end < dataHoje) {
            //event.color = "#77DD77"; //Concluído OK
            element.css('background-color', '#77DD77');
        } else if (event.start > dataHoje && event.end > dataHoje) {
            //event.color = "#AEC6CF"; //Não iniciado
            element.css('background-color', '#AEC6CF');
        }
    }
});
function GetValues() {
    GenerateRequest('GET', '/Agenda/GetValues', null, false);

    var evento = [];
    for (var i = 0; i < resultRequest.length; i++) {
        evento[evento.length] = { id: resultRequest[i].Id, title: resultRequest[i].ClienteNome, start: DateFormat(resultRequest[i].Data) };
    }
    return evento;
}

$('body').on('click', '#novo-cliente', function () {
    swal({
        title: "Cadastro rápido!",
        text: "Insira o cliente:",
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-top",
        inputPlaceholder: "Insira o nome"
    }, function (inputValue) {
        if (inputValue === false)
            return false;
        if (inputValue === "") {
            swal.showInputError("O campo está em branco :(");
            return false
        }
        var data = { nome: inputValue };
        GenerateRequest('GET', '/Cliente/CadastroRapido', data, false);
        if (resultRequest == 'True') {
            swal("Legal!", "Novo cliente cadastrado!", "success");
            GenerateRequest('GET', '/Cliente/GetJson', null, false);
            if (resultRequest != null) {
                GenerateClientList(resultRequest);
            }
        } else {
            swal("Oops!", "Ocorreu um erro ao gravar o cliente!", "error");
        }
    });
})

function GenerateClientList(data) {
    $('#ClienteSelected option').remove();
    var selecione = '<option value="0">Selecione</option>'
    $('#ClienteSelected').append(selecione);
    for (var i = 0; i < data.length; i++) {
        $('#ClienteSelected').append('<option value="' + data[i].Id + '">' + data[i].Contato.Nome + '</option>')
    }
}
/**************************END CALENDAR METHODS**************************************/
/*****************************EMPLOYEES METHODS**************************************/
$('body').on('focusout', '#DataDemissao', function () {
    if ($(this).val() == '' || $(this).val() == undefined) {
        return;
    }
    if ($(this).val() < $('#DataAdmissao').val()) {
        GenerateTimeMessage('Oops!', 'Data de demissão não pode ser inferior a data de admissão!', 'warning');
        $(this).val('');
        $(this).focus();
        return false;
    }
});
$('body').on('focusout', '#Cpf', function () {
    if (CpfValidade($(this).val()) == false) {
        GenerateTimeMessage('Oops!', 'CPF inválido', 'warning');
        $(this).focus();
        return false;
    }
});
/**************************END EMPLOYEES METHODS*************************************/
/**********************************CAIXA METHODS*************************************/
$('body').on('click', '#abrir-caixa', function () {
    if ($('#caixa-valor-inicial').val() != '') {
        swal({
            title: 'Deseja abrir o caixa?',
            text: 'Abrir o caixa permite realizar inúmeras funções gerenciais',
            type: 'info',
            showCancelButton: true,
            closeOnConfirm: false,
            showLoaderOnConfirm: true,
        }, function () {
            var data = { valorInicial: $('#caixa-valor-inicial').val() };
            GenerateRequest('GET', '/Pdv/AbrirCaixaValor', data, false);
            if (resultRequest == 'True') {
                GenerateTimeMessage('Uhul!', 'Caixa aberto com sucesso!', 'success');
                window.location = '/Pdv/Index';
            } else {
                GenerateTimeMessage('OOps!', 'Erro ao abrir caixa', 'error');
            }
        });
    } else {
        GenerateTimeMessage('OOps!', 'Não é possível abrir o caixa com valor inicial 0', 'warning');
        return;
    }
});

$('body').on('click', '#fechar-caixa', function () {
    swal({
        title: 'Deseja fechar o caixa?',
        text: 'Ao fechar o caixa, nenhum pedido poderá ser emitido',
        type: 'info',
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
    }, function () {
        var data = { valorFinal: $('#caixa-valor-final').val() };
        GenerateRequest('GET', '/Pdv/FecharCaixaValor', data, false);
        if (resultRequest == 'False') {
            GenerateTimeMessage('Uhul!', 'Caixa fechado com sucesso!', 'success');
            windows.location = 'Pdv/Index';
        } else {
            GenerateTimeMessage('OOps!', 'Erro ao fechar caixa', 'error');
        }
    });
});

$('body').on('focusout', '#rg-cliente', function () {
    resultRequest = null;
    var data = { rg: $(this).val() };
    GenerateRequest('GET', '/Cliente/GetRg', data, false);
    if (resultRequest != "") {
        $('#id-cliente').val(resultRequest.Id);
        $('#nome-cliente').val(resultRequest.Contato.Nome);
        $('#telefone-cliente').val(resultRequest.Contato.Celular);
        $('#nascimento-cliente').val(FormatActualDate(DateFormat(resultRequest.DataNascimento)));
        $('#nome-cliente').prop('disabled', true);
        $('#telefone-cliente').prop('disabled', true);
        $('#nascimento-cliente').prop('disabled', true);
    } else {
        GenerateTimeMessage('Oops!', 'Cliente não cadastrado!', 'warning');
        $('#nome-cliente').prop('disabled', false);
        $('#nome-cliente').focus();
        $('#telefone-cliente').prop('disabled', false);
        $('#nascimento-cliente').prop('disabled', false);
        $('#id-cliente').val('');
        $('#nome-cliente').val('');
        $('#telefone-cliente').val('');
        $('#nascimento-cliente').val('');
    }
});

$('body').on('click', '#entrada-cliente', function () {
    var nascimento = $('#nascimento-cliente').val().split('-');
    var data = { id: $('#id-cliente').val(), rg: $('#rg-cliente').val(), nome: $('#nome-cliente').val(), telefone: $('#telefone-cliente').val(), nascimento: nascimento[0].replace(/ /g, ''), numeroCartao: $('#cartao-entrada-cliente').val() };
    GenerateRequest('GET', '/Pdv/EntradaClienteCadastro', data, false);
    if (resultRequest == 1) {
        GenerateTimeMessage('Uhul!', 'Entrada liberada!', 'success');
        window.location.reload();
    } else {
        if (resultRequest == -1) {
            GenerateTimeMessage('Oops!', 'Cartão já registrado!', 'warning');
            $('#cartao-entrada-cliente').val('');
            $('#cartao-entrada-cliente').focus();
        } else if (resultRequest == -2) {
            GenerateTimeMessage('Oops!', 'Estabelecimento cheio!', 'warning');
        }
    }
});

$('body').on('click', '#reforco-caixa', function () {
    if ($('#reforco-valor-caixa').val() != '' && $('#motivo-reforco-caixa').val() != '') {
        swal({
            title: 'Deseja inserir reforço?',
            text: 'Inserir reforços ao caixa permite que o processo de venda continue',
            type: 'info',
            showCancelButton: true,
            closeOnConfirm: false,
            showLoaderOnConfirm: true,
        }, function () {
            var data = { valorReforco: $('#reforco-valor-caixa').val(), motivo : $('#motivo-reforco-caixa').val() };
            GenerateRequest('GET', '/Pdv/AdicionarReforco', data, false);
            if (resultRequest == 'True') {
                GenerateTimeMessage('Uhul!', 'Reforço inserido com sucesso!', 'success');
                window.location = '/Pdv/Index';
            } else {
                GenerateTimeMessage('OOps!', 'Erro ao efetuar reforço do caixa', 'error');
            }
        });
    } else {
        GenerateTimeMessage('OOps!', 'Campos obrigatórios não preenchidos', 'warning');
    }
});

$('body').on('click', '#sangria-caixa', function () {
    if ($('#sangria-valor-caixa').val() != '' && $('#sangria-reforco-caixa').val() != '') {
        swal({
            title: 'Deseja efetuar a sangria?',
            text: 'Dica: Remova valores do caixa e guarde em locais seguros!',
            type: 'info',
            showCancelButton: true,
            closeOnConfirm: false,
            showLoaderOnConfirm: true,
        }, function () {
            var data = { valorSangria: $('#sangria-valor-caixa').val(), motivo: $('#motivo-sangria-caixa').val() };
            GenerateRequest('GET', '/Pdv/EfetuarSangria', data, false);
            if (resultRequest == 'True') {
                GenerateTimeMessage('Uhul!', 'Sangria efetuada com sucesso!', 'success');
                window.location = '/Pdv/Index';
            } else {
                GenerateTimeMessage('OOps!', 'Erro efetuar sangria do caixa', 'error');
            }
        });
    } else {
        GenerateTimeMessage('OOps!', 'Campos obrigatórios não preenchidos', 'warning');
    }
});

$('body').on('focusout', '#saida-cartao-cliente', function () {
    var data = { numeroCartao: $(this).val() };
    GenerateRequest('GET', '/Pdv/Fechamento', data, false);
    if (resultRequest.length != 0) {
        $('#saida-nome-cliente').val(resultRequest[0].Nome);
        var total = 0;
        for (var i = 0; i < resultRequest.length; i++) {
            total += resultRequest[i].TOTAL;
            $('#produtos tbody').append('<tr><td>' + resultRequest[i].Produto + '</td><td> ' + resultRequest[i].Quantidade + '</td><td>' + resultRequest[i].TOTAL + '</td></tr>')
        }
        $('#saida-total-cliente').val('R$ ' + total);
        $('#troco').val(0);
        $('#valor-recebido').prop('disabled', false);
        $('#valor-recebido').focus();
    } else {
        GenerateTimeMessage('Oops!', 'Cartão não encontrado!', 'warning');
    }
});

$('body').on('focusout', '#valor-recebido', function () {
    var recebido = $(this).val().replace(',', '.');
    $('#troco').val('');
    var total = $('#saida-total-cliente').val().replace('R$', '').replace(' ', '');
    if (recebido > total){
        $('#troco').val(recebido - total);
    }
    else if(recebido < total){
        $('#troco').val('Faltando: ' + (total - recebido));
    }else{
        $('#troco').val('0');
    }
});

$('body').on('click', '#click-total', function () {
    if ($('#saida-total-cliente').prop('disabled') == false) {
        $('#saida-total-cliente').prop('disabled', true);
    } else {
        $('#saida-total-cliente').prop('disabled', false);
        $('#saida-total-cliente').focus();
    }
});

$('body').on('focusout', '#saida-total-cliente', function () {
    $(this).prop('disabled', true);
    $('#valor-recebido').val('');
    $('#troco').val('');
});
var formaPagamento = '';
$('body').on('click', '#cartao-credito', function () {
    formaPagamento = $(this).text();
});
$('body').on('click', '#cartao-debito', function () {
    formaPagamento = $(this).text();
});
$('body').on('click', '#dinheiro', function () {
    formaPagamento = $(this).text();
});
$('body').on('click', '#cheque', function () {
    formaPagamento = $(this).text();
});

$('body').on('click', '#saida-registrar-cliente', function () {
    var data = {
        numeroCartao: $('#saida-cartao-cliente').val(),
        valorTotal: $('#saida-total-cliente').val().replace('R$', '').replace(' ', ''),
        valorRecebido: $('#valor-recebido').val(),
        troco: $('#troco').val().replace('Faltando: -', ''),
        formaPagamento: formaPagamento
    };
    GenerateRequest('GET', '/Pdv/FecharComanda', data, false);
    GenerateTimeMessage('Ok!', 'Saída registrada', 'success');
    window.location.reload();
});
/******************************END CAIXA METHODS*************************************/

/******************************PEDIDOS METHODS*************************************/

$('body').on('focusout', '#codigo-cliente', function () {
    var data = { numeroCartao: $(this).val() };
    GenerateRequest('GET', '/Pdv/GetClienteCartao', data, false);
    if (resultRequest == null || resultRequest == 'null' || resultRequest == ''){
        GenerateTimeMessage('Oops!', 'Cliente não encontrado!', 'warning');
        $(this).val('');
        $(this).focus();
    }
    else {
        $('#nome-cliente').val(resultRequest);
        $('#codigo-funcionario').prop('disabled', false);
        $('#codigo-funcionario').focus();
    }
});

$('body').on('focusout', '#codigo-funcionario', function () {
    var data = { idFuncionario: $(this).val() };
    GenerateRequest('GET', '/Pdv/GetFuncionarioId', data, false);
    if (resultRequest != null) {
        $('#nome-funcionario').val(resultRequest[0].Contato.Nome);
        $('#codigo-produto').prop('disabled', false);
        $('#codigo-produto').focus();
    } else {
        GenerateTimeMessage('Oops', 'Atendente não encontrado', 'warning');
        $(this).val('');
        $(this).focus();
    }
});

$('body').on('focusout', '#codigo-produto', function () {
    var data = { idProduto: $(this).val() };
    GenerateRequest('GET', '/Pdv/GetProdutoId', data, false);
    if (resultRequest.Nome != null) {
        $(this).val(resultRequest.Nome);
        $('#quantidade-produto').prop('disabled', false);
        $('#quantidade-produto').focus();
    } else {
        GenerateTimeMessage('Oops', 'Produto não encontrado', 'warning');
        $(this).val('');
        $(this).focus();
    }
});

$('body').on('click', '#fechar-pedido', function () {
    var data = { cartaoCliente: $('#codigo-cliente').val(), codigoAtendente: $('#codigo-funcionario').val(), nomeProduto: $('#codigo-produto').val(), quantidade: $('#quantidade-produto').val() };
    GenerateRequest('GET', '/Pdv/InserirPedido', data, false);
    if (resultRequest == true) {
        GenerateTimeMessage('Legal!', 'Pedido realizado com sucesso!', 'success');
        window.location.reload();
    } else {
        GenerateTimeMessage('Oops!', 'Erro ao realizar pedido!', 'error');
    }
        
});
/**************************END PEDIDOS METHODS*************************************/
/***************************LISTA COMPRA METHODS************************************/
CarregaProdutos();
var produtos = null;
function CarregaProdutos() {
    $.ajax({
        type: 'GET',
        url: '/Produto/GetJson',
        data: null,
        async: true,
        cache: false,
        success: function (data) {
            if (data != null) {
                produtos = data;
                CarregaComboProdutos();
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
function CarregaComboProdutos() {
    for (var i = 0; i < produtos.length; i++) {
        $('#lista-produto').append('<option value="' + produtos[i].Id + '">' + produtos[i].Nome + '</option>');
    }
}
$('body').on('click', '#adicionar-produto-lista', function () {
    if ($('#lista-produto').val() == '0') {
        GenerateTimeMessage('Oops!', 'Selecione um produto', 'warning');
        $('#lista-produto').focus();
        return;
    }
    if ($('#quantidade-produto-lista').val() == '') {
        GenerateTimeMessage('Oops!', 'Insira uma quantidade', 'warning');
        return;
    }
        
    $('#produtos-compra').append('<tr><td class="id-produto">' + $("#lista-produto option:selected").val() + '</td><td class="nome-produto">' + $("#lista-produto option:selected").text() + '</td><td class="quant-produto">' + $('#quantidade-produto-lista').val() + '</td><td><button type="button" class="btn btn-danger" id="excluir-produto-lista">Excluir</button></td></tr>');
    $('#lista-produto').val(0);
    $('#quantidade-produto-lista').val('');
});
$('body').on('click', '#excluir-produto-lista', function () {
    $(this).parent().parent().remove();
});

$('body').on('click', '#salvar-lista', function () {
    var listas = [];
    var linhas = $('#produtos-compra').children();
    for (var i = 0; i < linhas.length; i++) {

        var id = $(linhas[i]).children()[0];
        var quantidade = $(linhas[i]).children()[2];

        listas[listas.length] = new Lista($(id).html(), $(quantidade).html());
    }
    var data = { lista: JSON.stringify(listas) }
    $.ajax({
        type: 'GET',
        url: '/ListaCompras/CadastrarListaCompra',
        data: data,
        async: true,
        cache: false,
        success: function (data) {
            if (data != null) {
                GenerateTimeMessage('Uhul!', 'Lista cadastrada', 'success');
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
});

function Lista(id, quantidade) {
    this.id = id;
    this.quantidade = quantidade;
}
/************************END LISTA COMPRA METHODS***********************************/

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
function CpfValidade(cpf) {
    cpf = cpf.replace(/[^\d]+/g, '');
    var Soma;
    var Resto;
    Soma = 0;
    if (cpf == "00000000000")
        return false;
    for (i = 1; i <= 9; i++)
        Soma = Soma + parseInt(cpf.substring(i - 1, i)) * (11 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11))
        Resto = 0;

    if (Resto != parseInt(cpf.substring(9, 10)))
        return false;
    Soma = 0;
    for (i = 1; i <= 10; i++)
        Soma = Soma + parseInt(cpf.substring(i - 1, i)) * (12 - i);
    Resto = (Soma * 10) % 11;
    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(cpf.substring(10, 11)))
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
function GenerateTimeMessage(title, message, type) {
    swal({ title: title, text: message, type: type, timer: 2000, showConfirmButton: false });
}
function GenerateMessage(title, message, type) {
    swal({ title: title, text: message, type: type });
}
function DateFormat(data) {
    return new Date(parseInt(data.replace('Date', '').replace('/', '').replace('(', '').replace(')', '').replace('/', '')));
}
function FormatActualDate(dateToFormat) {
    var dateValue = new Date(dateToFormat);
    var monthValue = dateValue.getMonth() + 1;
    var dayValue = dateValue.getDate();
    var yearValue = dateValue.getFullYear();
    var hoursValue = dateValue.getHours();
    var minutesValue = dateValue.getMinutes();
    var secondsValue = dateValue.getSeconds();

    if (monthValue < 10)
        monthValue = '0' + monthValue;
    if (dayValue < 10)
        dayValue = '0' + dayValue;
    if (hoursValue < 10)
        hoursValue = '0' + hoursValue;
    if (minutesValue < 10)
        minutesValue = '0' + minutesValue;
    if (secondsValue < 10)
        secondsValue = '0' + secondsValue;

    // dd-mm-yyyy-hh-mm-ss
    return (dayValue + '/' + monthValue + '/' + yearValue + ' - ' + hoursValue + ':' + minutesValue);
}
/********************************END AUX METHODS**************************************/
//var db = openDatabase("HMALite", "1.0", "HMALite - Arquivos locais", 200000);

//db.transaction(function (transaction) {
//    transaction.executeSql('CREATE TABLE IF NOT EXISTS User ("id" INTEGER PRIMARY KEY, "Nome" TEXT NOT NULL, "Email" TEXT NOT NULL)', [], null, db.onError);
//})

//// função callback de erro
//db.onError = function (transaction, e) {
//    alert("Aconteceu um erro: " + e.message);
//    console.log(e.message);
//}

//// função de callback de sucesso de insert
//db.onSuccess = function (transaction, e) {
//    alert("Dados Gravados com Sucesso!");
//    console.log(e);
//}

//// função temporaria que lista resultados
//db.getResults = function (transaction, r) {
//    console.log('deu certo!');
//    console.log(r);

//    for (var i = 0; i < r.rows.length; i++) {
//        console.log(r.rows.item(i)[['Id']]);
//        console.log(r.rows.item(i)[['Nome']]);
//        console.log(r.rows.item(i)[['Email']]);
//    }
//}

//// aqui vai o insert
//db.transaction(function (transaction) {
//    transaction.executeSql("INSERT INTO User(Id,Nome, Email) VALUES(?, ?, ?)", [1, 'Giuliano Costa', 'giulianocosta@outlook.com'], db.onSuccess, db.onError);
//})

//// consulta no banco
//db.transaction(function (transaction) {
//    transaction.executeSql("SELECT * FROM User", [], db.getResults, db.onError);
//})