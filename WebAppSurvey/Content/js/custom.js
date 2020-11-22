var validAjax;
var formAjax2 = $('.formAjax2');
$("button.btnAjax2").on('click', function (e) {
    e.preventDefault();
    validAjax = formAjax2.validate({
        //== Validate only visible fields
        ignore: ":hidden",
        //== Display error  
        invalidHandler: function (event, validAjax) {
            swal({
                "title": "",
                "text": "Los datos capturados no son correctos.",
                "type": "error",
                "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
            });
        },
        //== Submit valid form
        submitHandler: function (form) {
        }
    });
    if (validAjax.form() == false) {
        $("#userSubs-error").hide();
        return;
    }
    $(".loadingAjax2").show();
    formAjax2.ajaxSubmit({
        success: function (response) {
            if (response.IsSuccess == true) {
                if (response.Id == -1) {
                    window.location.href = response.Message;
                } else {
                    swal({
                        "title": "",
                        "text": response.Message,
                        "type": "success",
                        "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                    }).then(function () {
                        //$(formAjax).find("input[type=text]").val("");
                        //$(formAjax).find("select").val("");
                        $(".ocultarDespues2").modal();
                        //$("#IdMod").val(response.Id);
                        //window.location = urlGeneral + "tramites";
                    });
                }
            }
            $(".loadingAjax2").hide();
        },
        error: function (request, status, error) {
            swal({
                "title": "",
                "text": "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
                "type": "error",
                "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
            }).then(function () {
                //window.location = urlGeneral + "tramites";
            });
            $(".loadingAjax2").hide();
        }
    });
});



var validAjax;
var formAjax = $('.formAjax');
$("button.btnAjax").on('click', function (e) {
    e.preventDefault();
    validAjax = formAjax.validate({
        //== Validate only visible fields
        ignore: ":hidden",
        //== Display error  
        invalidHandler: function (event, validAjax) {
            swal({
                "title": "",
                "text": "Los datos capturados no son correctos.",
                "type": "error",
                "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
            });
        },
        //== Submit valid form
        submitHandler: function (form) {
        }
    });
    if (validAjax.form() == false) {
        $("#userSubs-error").hide();
        return;
    }
    $(".loadingAjax").show();
    formAjax.ajaxSubmit({
        success: function (response) {
            if (response.IsSuccess == true) {
                if (response.Id == -1) {
                    window.location.href = response.Message;
                } else {
                    swal({
                        "title": "",
                        "text": response.Message,
                        "type": "success",
                        "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                    }).then(function () {
                        //$(formAjax).find("input[type=text]").val("");
                        //$(formAjax).find("select").val("");
                        $(".ocultarDespues").modal();
                        //$("#IdMod").val(response.Id);
                        //window.location = urlGeneral + "tramites";
                    });
                }
            }
            $(".loadingAjax").hide();
        },
        error: function (request, status, error) {
            swal({
                "title": "",
                "text": "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
                "type": "error",
                "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
            }).then(function () {
                //window.location = urlGeneral + "tramites";
            });
            $(".loadingAjax").hide();
        }
    });
});


//forms add
var validAjaxAdd;
var formAjaxAdd = $('.formAjaxAdd');
$("button.btnAjaxAdd").on('click', function (e) {
    e.preventDefault();
    validAjaxAdd = formAjaxAdd.validate({
        //== Validate only visible fields
        ignore: ":hidden",
        //== Display error  
        invalidHandler: function (event, validAjax) {
            swal({
                "title": "",
                "text": "Los datos capturados no son correctos.",
                "type": "error",
                "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
            });
        },
        //== Submit valid form
        submitHandler: function (form) {
        }
    });
    if (validAjaxAdd.form() == false) {
        $("#userSubs-error").hide();
        return;
    }
    $(".loadingAjaxAdd").show();
    formAjaxAdd.ajaxSubmit({
        success: function (response) {
            if (response.IsSuccess == true) {
                swal({
                    "title": "",
                    "text": response.Message,
                    "type": "success",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    $(".ocultarDespues").modal('hide');
                    cargaTabla(strFiltro, strValOrder);
                    removeInfoForm(formAjaxAdd);

                    //$("#IdMod").val(response.Id);
                    //window.location = urlGeneral + "tramites";
                });
            }
            $(".loadingAjaxAdd").hide();
        },
        error: function (request, status, error) {
            swal({
                "title": "",
                "text": "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
                "type": "error",
                "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
            }).then(function () {
                //window.location = urlGeneral + "tramites";
            });
            $(".loadingAjaxAdd").hide();
        }
    });
});

function removeInfoForm(formAjaxAdd) {
    $(formAjaxAdd).find("input[type=text]").val("");
    $(formAjaxAdd).find("input[type=number]").val("");
    $(formAjaxAdd).find("input[type=number]")
    $("#modal-dropzone").find("input[type=file]").empty();
    $("#modal-dropzone").removeClass("dz-started");
    $(".dz-preview").remove();
    $(formAjaxAdd).find("select").val("");
}


function funEncuestado(id, tipo, evt) {
    evt.preventDefault();
    if (tipo == 'E') {
        $.ajax({
            type: "POST",
            url: urlGeneral + "Encuestados/Eliminar",
            data: "{Id: " + id + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                swal({
                    "title": "",
                    "text": response.Message,
                    "type": "success",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    $(".ocultarDespues").modal('hide');
                    cargaTabla(strFiltro, strValOrder);

                });
                $(".loadingAjaxAdd").hide();
            },
            error: function (request, status, error) {
                swal({
                    "title": "",
                    "text": "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    //window.location = urlGeneral + "tramites";
                });
                $(".loadingAjaxAdd").hide();
            }
        });
    } else {
        $("#modal8").modal("show");
        $('#mensaje').text("Actualizar");
        $('#botonMensaje').text("Actualizar");
        $.ajax({
            type: "POST",
            url: urlGeneral + "Encuestados/Get",
            data: "{Id: " + id + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.IsSuccess == true) {
                    $("#Dni").val(response.Result.Dni);
                    $("#Nombres").val(response.Result.Nombres);
                    $("#ApellidoPatern").val(response.Result.ApellidoPaterno);
                    $("#ApellidoMatern").val(response.Result.ApellidoMaterno);
                    $("#Direccion").val(response.Result.Direccion);
                    $("#Edad").val(response.Result.Edad);
                    $("#Sexo").val(response.Result.Sexo);
                    $("#Telefon").val(response.Result.Telefono);
                    $("#Correo").val(response.Result2.Correo);
                    $("#Id").val(response.Result2.Id);
                    $("#NombreUsu").val(response.Result2.NombreUsuario);
                    $("#TipoUsu").val(response.Result2.TipoUsuario);
                    removeInfoForm(formAjaxAdd);

                } else {
                    swal({
                        "title": "",
                        "text": response.Message,
                        "type": "error",
                        "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                    }).then(function () {
                        //window.location = urlGeneral + "tramites";
                    });
                }
            },
            error: function (request, status, error) {
                swal({
                    "title": "",
                    "text": "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    //window.location = urlGeneral + "tramites";
                });
                $(".loadingAjaxAdd").hide();
            }
        });
    }
}

function funCoordinador(id, tipo, evt) {
    evt.preventDefault();
    if (tipo == 'E') {
        $.ajax({
            type: "POST",
            url: urlGeneral + "Coordinador/Eliminar",
            data: "{Id: " + id + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                swal({
                    "title": "",
                    "text": response.Message,
                    "type": "success",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    $(".ocultarDespues").modal('hide');
                    cargaTabla(strFiltro, strValOrder);

                });
                $(".loadingAjaxAdd").hide();
            },
            error: function (request, status, error) {
                swal({
                    "title": "",
                    "text": "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    //window.location = urlGeneral + "tramites";
                });
                $(".loadingAjaxAdd").hide();
            }
        });
    } else {
        $("#modal8").modal("show");
        $.ajax({
            type: "POST",
            url: urlGeneral + "Coordinador/Get",
            data: "{Id: " + id + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.IsSuccess == true) {
                    $("#Dni").val(response.Result.Dni);
                    $("#Nombres").val(response.Result.Nombres);
                    $("#ApellidoPatern").val(response.Result.ApellidoPaterno);
                    $("#ApellidoMatern").val(response.Result.ApellidoMaterno);
                    $("#Direccion").val(response.Result.Direccion);
                    $("#Edad").val(response.Result.Edad);
                    $("#Sexo").val(response.Result.Sexo);
                    $("#Telefon").val(response.Result.Telefono);
                    $("#CorreoUser").val(response.Result2.Correo);
                    $("#NombreUser").val(response.Result2.NombreUsuario);
                    $("#TipoUsuarios").val(response.Result2.TipoUsuario);
                    $("#Id").val(response.Result2.Id);
                    removeInfoForm(formAjaxAdd);

                } else {
                    swal({
                        "title": "",
                        "text": response.Message,
                        "type": "error",
                        "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                    }).then(function () {
                        //window.location = urlGeneral + "tramites";
                    });
                }
            },
            error: function (request, status, error) {
                swal({
                    "title": "",
                    "text": "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    //window.location = urlGeneral + "tramites";
                });
                $(".loadingAjaxAdd").hide();
            }
        });
    }
}

function funPacientes(id, tipo, evt) {
    evt.preventDefault();
    if (tipo == 'E') {
        $.ajax({
            type: "POST",
            url: urlGeneral + "Pacientes/Eliminar",
            data: "{Id: " + id + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                swal({
                    "title": "",
                    "text": response.Message,
                    "type": "success",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    $(".ocultarDespues").modal('hide');
                    cargaTabla(strFiltro, strValOrder);

                });
                $(".loadingAjaxAdd").hide();
            },
            error: function (request, status, error) {
                swal({
                    "title": "",
                    "text": "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    //window.location = urlGeneral + "tramites";
                });
                $(".loadingAjaxAdd").hide();
            }
        });
    } else {
        $("#modal8").modal("show");
        $.ajax({
            type: "POST",
            url: urlGeneral + "Pacientes/Get",
            data: "{Id: " + id + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.IsSuccess == true) {
                    $("#IdPaciente").val(response.Result.IdPaciente);
                    $("#Dni").val(response.Result.Dni);
                    $("#Nombres").val(response.Result.Nombres);
                    $("#Apaterno").val(response.Result.ApellidoPaterno);
                    $("#Amaterno").val(response.Result.ApellidoMaterno);
                    $("#Profesion").val(response.Result.Profesion);
                    $("#Direccion").val(response.Result.Direccion);
                    $("#Edad").val(response.Result.Edad);                    
                    $("#Sexo").val(response.Result.Sexo);                    
                    $("#telefono").val(response.Result.Telefono);
                    $("#FechaNacimiento").val(response.Result3.toLocaleString('en-GB'));
                    $("#Correo").val(response.Result2.Correo);
                    $("#NombreUsu").val(response.Result2.NombreUsuario);
                    $("#TipoUsu").val(response.Result2.TipoUsuario);
                    $("#IdUsuario").val(response.Result2.IdUsuario);
                    removeInfoForm(formAjaxAdd);

                } else {
                    swal({
                        "title": "",
                        "text": response.Message,
                        "type": "error",
                        "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                    }).then(function () {
                        //window.location = urlGeneral + "tramites";
                    });
                }
            },
            error: function (request, status, error) {
                swal({
                    "title": "",
                    "text": "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    //window.location = urlGeneral + "tramites";
                });
                $(".loadingAjaxAdd").hide();
            }
        });
    }
}


function funUsuario2(id, tipo, evt) {
    evt.preventDefault();
    if (tipo == 'E') {
        $.ajax({
            type: "POST",
            url: urlGeneral + "Users/Eliminar",
            data: "{Id: " + id + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                swal({
                    "title": "",
                    "text": response.Message,
                    "type": "success",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    $(".ocultarDespues").modal('hide');
                    cargaTabla(strFiltro, strValOrder);

                });
                $(".loadingAjaxAdd").hide();
            },
            error: function (request, status, error) {
                swal({
                    "title": "",
                    "text": "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    //window.location = urlGeneral + "tramites";
                });
                $(".loadingAjaxAdd").hide();
            }
        });
    } else {
        $("#modal32").modal("show");
        //document.getElementById("TipoUsu").disabled = true;
        $.ajax({
            type: "POST",
            url: urlGeneral + "Users/Get",
            data: "{Id: " + id + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.IsSuccess == true) {
                    $("#DniUsu").val(response.Result.Dni);
                    $("#NombresUsu").val(response.Result.Nombres);
                    $("#ApellidoPaterno").val(response.Result.ApellidoPaterno);
                    $("#ApellidoMaterno").val(response.Result.ApellidoMaterno);
                    $("#DireccionUsu").val(response.Result.Direccion);
                    $("#EdadUsu").val(response.Result.Edad);
                    $("#SexoUsu").val(response.Result.Sexo);
                    $("#Telefono").val(response.Result.Telefono);
                    $("#CorreoUsu").val(response.Result2.Correo);
                    $("#IdUsu").val(response.Result2.Id);
                    $("#NombreUsuario").val(response.Result2.NombreUsuario);
                    $("#TipoUsu").val(response.Result2.TipoUsuario);
                    //removeInfoForm(formAjaxAdd);

                } else {
                    swal({
                        "title": "",
                        "text": response.Message,
                        "type": "error",
                        "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                    }).then(function () {
                        //window.location = urlGeneral + "tramites";
                    });
                }
            },
            error: function (request, status, error) {
                swal({
                    "title": "",
                    "text": "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    //window.location = urlGeneral + "tramites";
                });
                $(".loadingAjaxAdd").hide();
            }
        });
    }
}



function funUsuario(id, tipo, evt) {
    evt.preventDefault();
    if (tipo == 'E') {
        $.ajax({
            type: "POST",
            url: urlGeneral + "Users/Eliminar",
            data: "{Id: " + id + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                swal({
                    "title": "",
                    "text": response.Message,
                    "type": "success",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    $(".ocultarDespues").modal('hide');
                    cargaTabla(strFiltro, strValOrder);

                });
                $(".loadingAjaxAdd").hide();
            },
            error: function (request, status, error) {
                swal({
                    "title": "",
                    "text": "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    //window.location = urlGeneral + "tramites";
                });
                $(".loadingAjaxAdd").hide();
            }
        });
    } else {
        $("#modal8").modal("show");
        //document.getElementById("TipoUsuarios").disabled = true;
        $.ajax({
            type: "POST",
            url: urlGeneral + "Users/Get",
            data: "{Id: " + id + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.IsSuccess == true) {
                    $("#Dni").val(response.Result.Dni);
                    $("#Nombres").val(response.Result.Nombres);
                    $("#ApellidoPatern").val(response.Result.ApellidoPaterno);
                    $("#ApellidoMatern").val(response.Result.ApellidoMaterno);
                    $("#Direccion").val(response.Result.Direccion);
                    $("#Edad").val(response.Result.Edad);
                    $("#Sexo").val(response.Result.Sexo);
                    $("#Telefon").val(response.Result.Telefono);
                    $("#Correo").val(response.Result2.Correo);
                    $("#Id").val(response.Result2.Id);
                    $("#NombreUsu").val(response.Result2.NombreUsuario);
                    $("#TipoUser").val(response.Result2.TipoUsuario);
                    removeInfoForm(formAjaxAdd);

                } else {
                    swal({
                        "title": "",
                        "text": response.Message,
                        "type": "error",
                        "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                    }).then(function () {
                        //window.location = urlGeneral + "tramites";
                    });
                }
            },
            error: function (request, status, error) {
                swal({
                    "title": "",
                    "text": "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    //window.location = urlGeneral + "tramites";
                });
                $(".loadingAjaxAdd").hide();
            }
        });
    }
}

function funDiag(id, tipo, evt) {
    evt.preventDefault();
    if (tipo == 'E') {
        $.ajax({
            type: "POST",
            url: urlGeneral + "Diagnostico/Eliminar",
            data: "{Id: " + id + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                swal({
                    "title": "",
                    "text": response.Message,
                    "type": "success",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    $(".ocultarDespues").modal('hide');
                    cargaTabla(strFiltro, strValOrder);

                });
                $(".loadingAjaxAdd").hide();
            },
            error: function (request, status, error) {
                swal({
                    "title": "",
                    "text": "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    //window.location = urlGeneral + "tramites";
                });
                $(".loadingAjaxAdd").hide();
            }
        });
    }
    else if (tipo == 'F') {
        $("#modal21").modal("show");
        document.getElementById("DniPaciente").disabled = true;
        document.getElementById("NombresP").disabled = true;
        document.getElementById("ApellidoP").disabled = true;
        document.getElementById("ApellidoM").disabled = true;
    $.ajax({
        type: "POST",
        url: urlGeneral + "Diagnostico/Get",
        data: "{Id: " + id + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.IsSuccess == true) {
                $("#IdDiagnostico1").val(response.Result.IdDiagnostico);
                $("#NombresP").val(response.Result2);
                $("#ApellidoP").val(response.Result3);
                $("#ApellidoM").val(response.Result4);
                $("#Evitativo").val(response.Result.Evitativo);
                
                
                var canvas = document.getElementById("Grafica").getContext("2d");
                var esqui = document.getElementById("Esquizoide").value;

                if (window.grafica) {
                    window.grafica.clear();
                    window.grafica.destroy();
                }
               
                window.grafica = new Chart(canvas, {
                    type: 'horizontalBar',
                    data: {
                        labels: ["Esquizoide",
                                "Evitativo",
                                "Depresivo",
                                "Dependiente",
                                "Histriónico",
                                "Narcisista",
                                "Esquizotípica",
                                "Límite",
                                "Paranoide",
                                "Ansiedad",
                                "Somatoformo",
                                "Bipolar",
                                "Distímico",
                                "DependenciaAlcohol",
                                "DependenciaSustancias",
                                "EstrésPostraumático",
                                "DesordenPensamiento",
                                "DepresiónMayor",
                                "DesordenDelusional",
                                "Sinceridad",
                                "DeseabilidadSocial",
                                "Devaluación",
                                "Validez",
                                "Autodestructiva"


                               ],
                        datasets: [
                            {
                                label: "Diagnosticos",
                                backgroundColor: ['rgb(255, 99, 132)', 'rgb(0, 255, 0)', 'rgb(255, 99, 132)', 'rgb(128, 255, 0)', 'rgb(145,60, 255)', 'rgb(255, 255,80 )', 'rgb(255, 255, 128)', 'rgb(255, 99, 200)', 'rgb(20,179, 255)', 'rgb(20,179, 255)', 'rgb(20,179, 255)', 'rgb(20,179, 255)', 'rgb(20,179, 255)', 'rgb(20,179, 255)', 'rgb(20,179, 255)', 'rgb(20,179, 255)', 'rgb(20,179, 255)', 'rgb(20,179, 255)', 'rgb(20,179, 255)', 'rgb(20,179, 255)', 'rgb(20,179, 255)', 'rgb(180,255,200)', 'rgb(180,255, 200)','rgb(180,255, 200)', ],
                                borderColor: window.chartColors.red,
                                data: [response.Result.Esquizoide,
                                    response.Result.Evitativo,
                                    response.Result.Depresivo,
                                    response.Result.Dependiente,
                                    response.Result.Histriónico,
                                    response.Result.Narcisista,
                                    response.Result.Esquizotípica,
                                    response.Result.Límite,
                                    response.Result.Paranoide,
                                    response.Result.Ansiedad,
                                    response.Result.Somatoformo,
                                    response.Result.Bipolar,
                                    response.Result.Distímico,
                                    response.Result.DependenciaAlcohol,
                                    response.Result.DependenciaSustancias,
                                    response.Result.EstrésPostraumático,
                                    response.Result.DesordenPensamiento,
                                    response.Result.DepresiónMayor,
                                    response.Result.DesordenDelusional,
                                    response.Result.Sinceridad,
                                    response.Result.DeseabilidadSocial,
                                    response.Result.Devaluación,
                                    response.Result.Validez,
                                    response.Result.Autodestructiva

                                ],
                            }
                        ]
                    }
                })
                

            } else {
                swal({
                    "title": "",
                    "text": response.Message,
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    //window.location = urlGeneral + "tramites";
                });
            }
        },
        error: function (request, status, error) {
            swal({
                "title": "",
                "text": "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
                "type": "error",
                "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
            }).then(function () {
                //window.location = urlGeneral + "tramites";
            });
            $(".loadingAjaxAdd").hide();
        }
    });
}


    else {
        $("#modal8").modal("show");
        document.getElementById("DniPaciente").disabled = true;
        document.getElementById("NombresPa").disabled = true;
        document.getElementById("ApellidoPa").disabled = true;
        document.getElementById("ApellidoMa").disabled = true;
        $.ajax({
            type: "POST",
            url: urlGeneral + "Diagnostico/Get",
            data: "{Id: " + id + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.IsSuccess == true) {
                    $("#IdDiagnostico").val(response.Result.IdDiagnostico);
                    $("#NombresPa").val(response.Result2);
                    $("#ApellidoPa").val(response.Result3);
                    $("#ApellidoMa").val(response.Result4);
                    $("#DniPaciente").val(response.Result.DniPaciente);
                    $("#Esquizoide").val(response.Result.Esquizoide);
                    $("#Evitativo").val(response.Result.Evitativo);
                    $("#Depresivo").val(response.Result.Depresivo);
                    $("#Dependiente").val(response.Result.Dependiente);
                    $("#Histriónico").val(response.Result.Histriónico);
                    $("#Narcisista").val(response.Result.Narcisista);
                    $("#Antisocial").val(response.Result.Antisocial);
                    $("#AgresivoSádico").val(response.Result.AgresivoSádico);
                    $("#Compulsivo").val(response.Result.Compulsivo);
                    $("#Negativista").val(response.Result.Negativista);
                    $("#Esquizotípica").val(response.Result.Esquizotípica);
                    $("#Límite").val(response.Result.Límite);
                    $("#Paranoide").val(response.Result.Paranoide);
                    $("#Ansiedad").val(response.Result.Ansiedad);
                    $("#Somatoformo").val(response.Result.Somatoformo);
                    $("#Bipolar").val(response.Result.Bipolar);
                    $("#Distímico").val(response.Result.Distímico);
                    $("#DependenciaAlcohol").val(response.Result.DependenciaAlcohol);
                    $("#DependenciaSustancias").val(response.Result.DependenciaSustancias);
                    $("#EstrésPostraumático").val(response.Result.EstrésPostraumático);
                    $("#DesordenPensamiento").val(response.Result.DesordenPensamiento);
                    $("#DepresiónMayor").val(response.Result.DepresiónMayor);
                    $("#DesordenDelusional").val(response.Result.DesordenDelusional);
                    $("#Sinceridad").val(response.Result.Sinceridad);
                    $("#DeseabilidadSocial").val(response.Result.DeseabilidadSocial);
                    $("#Devaluación").val(response.Result.Devaluación);
                    $("#Validez").val(response.Result.Validez);
                    $("#Autodestructiva").val(response.Result.Autodestructiva);
                    

                } else {
                    swal({
                        "title": "",
                        "text": response.Message,
                        "type": "error",
                        "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                    }).then(function () {
                        //window.location = urlGeneral + "tramites";
                    });
                }
            },
            error: function (request, status, error) {
                swal({
                    "title": "",
                    "text": "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    //window.location = urlGeneral + "tramites";
                });
                $(".loadingAjaxAdd").hide();
            }
        });
    }
}


function funProd(id, tipo, evt) {
    evt.preventDefault();
    if (tipo == 'E') {
        $.ajax({
            type: "POST",
            url: urlGeneral + "Historias/Eliminar",
            data: "{Id: " + id + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                swal({
                    "title": "",
                    "text": response.Message,
                    "type": "success",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    $(".ocultarDespues").modal('hide');
                    cargaTabla(strFiltro, strValOrder);
                    
                });
                $(".loadingAjaxAdd").hide();
            },
            error: function (request, status, error) {
                swal({
                    "title": "",
                    "text": "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    //window.location = urlGeneral + "tramites";
                });
                $(".loadingAjaxAdd").hide();
            }
        });
    } else {
        $("#modal8").modal("show");
        $.ajax({
            type: "POST",
            url: urlGeneral + "Historias/Get",
            data: "{Id: " + id + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.IsSuccess == true) {
                    $("#IdHistoria").val(response.Result.IdHistoria);
                    $("#textarea2").val(response.Result.Motivo);
                    $("#textarea3").val(response.Result.Tratamiento);
                    $("#textarea4").val(response.Result.Observacion);
                    $("#Diagnostico").val(response.Result.CodigoDiagnostico);
                    $("#DniPsicologo").val(response.Result.DniPsicologo);
                    removeInfoForm(formAjaxAdd);
                                       
                } else {
                    swal({
                        "title": "",
                        "text": response.Message,
                        "type": "error",
                        "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                    }).then(function () {
                        //window.location = urlGeneral + "tramites";
                    });
                }
            },
            error: function (request, status, error) {
                swal({
                    "title": "",
                    "text": "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    //window.location = urlGeneral + "tramites";
                });
                $(".loadingAjaxAdd").hide();
            }
        });
    }
}

function eliminarImagen(val, divEliminar) {
    $("#archivos").val($("#archivos").val().replace(val + "|", ""));
    $("#" + divEliminar).remove();
}

$('#modal8').on('hidden.bs.modal', function () {
    $("input").val("");
    $("#anexosUpload").empty();
});

$('#modal21').on('hidden.bs.modal', function () {
    $("input").val("");
    
});

