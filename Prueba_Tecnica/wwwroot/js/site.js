$(document).ready(function () {

    var quizz = {
        root: "Quizz",
        answers: {
            R1: null,
            R2: null,
            R3: null,
            R4: null,
            R5: null,
            R5_1: null,
            R5_2: null,
            R6: null,
            R7: null,
            R8: null,
        },
        table: new Tabulator("#Table", {
            layout: "fitColumns", //fit columns to width of table (optional)
            columns: [ //Define Table Columns
                { formatter: "rownum", hozAlign: "center", width: 40 },
                { title: "Id Quizz", field: "id", sorter: "string" },
                { title: "R1", field: "r1", sorter: "string" },
                { title: "R2", field: "r2", sorter: "string", width: 105 },
                { title: "R3", field: "r3", sorter: "string", width: 105 },
                { title: "R4", field: "r4", sorter: "string", width: 105 },
                { title: "R5", field: "r5", sorter: "string", width: 105 },
                { title: "R5_1", field: "r5_1", sorter: "string", width: 105 },
                { title: "R5_2", field: "r5_2", sorter: "string", width: 105 },
                { title: "R6", field: "r6", sorter: "string", width: 105 },
                { title: "R7", field: "r7", sorter: "string", width: 105 },
                { title: "R8", field: "r8", sorter: "string", width: 105 },
            ]
        }),
        nextQuestion: null,
        QuestionNumber: 2,
        type: "single",
        userInteraction: function () {
            $("#Decline").on('click', function () {
                $("#Start").hide();
                $("#ContentText").hide();
                $("#Decline").hide();
                $("#FirstQuestion").hide();
                $("#Thanks").html("Gracias por su tiempo")
            });
            $("#Start").on('click', function () {

                quizz.answers['R' + 1] = 'a';
                quizz.getQuizz(2);
                $("#Start").hide();
                $("#Decline").hide();
                $("#Next").removeClass('d-none');
                $("#Thanks").hide();
                $("#ContentText").html('Pregunta 2');
            });
            $("#Next").on('click', function () {
                if (quizz.type == "single") {
                    if (quizz.QuestionNumber != 6) {
                        quizz.answers['R' + quizz.QuestionNumber] = $("#ListAnswers").val();
                        var next = document.querySelector('#ListAnswers');
                        var id = next.options[next.selectedIndex].id;
                        quizz.nextQuestion = id.replace("NQ", "");
                    } else {
                        var valueInput = $("#Control").val();
                        var regex = /^\d+/;

                        if (regex.test(valueInput)) {
                            $("#Error").hide();
                            quizz.answers['R' + quizz.QuestionNumber] = valueInput;
                        } else {
                            $("#Error").html('Ingrese solo Números').show();
                            return;

                        }
                    }

                }
                if (quizz.type == "multiple") {
                    var numberAns = 'R' + quizz.QuestionNumber;
                    numberAns = numberAns.replace('.', '_');

                    quizz.answers[numberAns] = [];
                    var checks = document.getElementsByClassName("form-check-input");
                    for (var i = 0; i < checks.length; i++) {
                        if (checks[i].checked == true) {
                            quizz.answers[numberAns].push(checks[i].value);
                        }
                    }
                    if (quizz.answers[numberAns].length == 0) {
                        $("#Error").html('Seleccione al menos una opción').show();
                        return;
                    }
                    $("#Error").hide();
                }
                console.log(quizz.answers);
                quizz.getQuizz(quizz.nextQuestion);

            })
            $("#BoolA").on('click', function () {
                console.log(quizz.answers);
                if (quizz.QuestionNumber != 8) {

                    quizz.answers['R' + quizz.QuestionNumber] = $(this).val();
                    quizz.nextQuestion = $(this)[0].attributes[4].value;
                    quizz.getQuizz(quizz.nextQuestion);
                } else {
                    quizz.answers['R' + quizz.QuestionNumber] = $(this).val();
                    quizz.postQuizz();
                }
            });
            $("#BoolB").on('click', function () {
                console.log(quizz.answers);
                if (quizz.QuestionNumber != 8) {
                    quizz.answers['R' + quizz.QuestionNumber] = $(this).val();
                    quizz.nextQuestion = $(this)[0].attributes[5].value;
                    quizz.getQuizz(quizz.nextQuestion);

                } else {
                    quizz.answers['R' + quizz.QuestionNumber] = $(this).val();
                    quizz.postQuizz();
                }
            });
            $("#download").on('click', function () {
                quizz.table.download("xlsx", "data.xlsx", { sheetName: "Datos Reporte" });
            })
        },
        getQuizz: function (number) {
            $.ajax({
                type: "GET",
                url: "/api/" + this.root + "/getQuizz?id=" + number,
                async: true,
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response.status == "Ok") {
                        var data = response.data;
                        var answers = data.answers;
                        $("#FirstQuestion").html(data.description);
                        $("#Answers").hide();
                        $("#ListAnswers").hide();
                        $("#BoolA").hide();
                        $("#BoolB").hide();
                        var html = '';
                        if (data.type == "single") {
                            if (data.numberQuestion != 6) {
                                html += '<select class="form-select" aria-label="Default select example">';
                                answers.forEach(function (data2, index) {

                                    html += (index == 0) ? '<option value="' + data2.letter + '" id="NQ' + data2.nextquestion + '" selected>' + data2.description + '</option>' :
                                        '<option value="' + data2.letter + '" id="NQ' + data2.nextquestion + '" >' + data2.description + '</option>';
                                });
                                html += '</select>';
                                $("#ListAnswers").html(html).show();
                            } else {
                                html += '<div class="mb-3">' +
                                    '<label for="Control" class="form-label">Salario</label>' +
                                    '<input type="number" class="form-control" id="Control" placeholder="Salario" pattern="^[0-9]">' +
                                    '</div>';
                                $("#Answers").html(html).show();
                            }

                            $("#Next").show();
                        }
                        if (data.type == "bool") {
                            $("#BoolA").attr('value', answers[0].letter).attr('db-question', answers[0].nextquestion).html(answers[0].description).removeClass('d-none').show();
                            $("#BoolB").attr('value', answers[1].letter).attr('db-question', answers[1].nextquestion).html(answers[1].description).removeClass('d-none').show();
                            $("#Next").hide();
                            if (data.numberQuestion == 1) {
                                $("#BoolA").hide(); $("#BoolB").hide();
                            }
                        }
                        if (data.type == "multiple") {
                            answers.forEach(function (data2, index) {
                                html += '<div class="form-check">' +
                                    '<input class="form-check-input" type="checkbox" value="' + data2.letter + '" id="flexCheckDefault' + index + '">' +
                                    '<label class="form-check-label" for="flexCheckDefault' + index + '">' +
                                    data2.description +
                                    '</label>' +
                                    '</div>';
                            });
                            $("#Next").show();
                            $("#Answers").html(html).show();
                        }
                        quizz.type = data.type;
                        quizz.nextQuestion = data.numberNextQuestion;
                        quizz.QuestionNumber = data.numberQuestion;
                        $("#ContentText").html('Pregunta ' + quizz.QuestionNumber);
                    } else {
                        alert("Error, póngase en contacto con el administrador")
                    }
                },
                failure: function (response) {
                    alert("Content load failed: error internal code");
                },
                error: function (response) {
                    alert("Content load failed:  error in ajax petition");
                }
            });
        },
        postQuizz: function () {
            if (quizz.answers.R5_1 == null) {
                quizz.answers.R5_1 = [];
            }
            if (quizz.answers.R5_2 == null) {
                quizz.answers.R5_2 = [];
            }
            if (quizz.answers.R7 == null) {
                quizz.answers.R7 = [];
            }
            $.ajax({
                type: "POST",
                url: "/api/" + this.root + "/PostQuizz",
                data: JSON.stringify(quizz.answers),
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response.data) {
                        $("#Start").hide();
                        $("#ContentText").hide();
                        $("#Decline").hide();
                        $("#FirstQuestion").hide();
                        $("#Thanks").html("Gracias por su tiempo").show();
                        $("#BoolA").hide();
                        $("#BoolB").hide();

                    }
                },
                failure: function (response) {
                    alert("Content load failed: error internal code");
                },
                error: function (response) {
                    alert("Content load failed:  error in ajax petition");
                }
            });
        },
        getDataTable: function () {
            $.ajax({
                type: "GET",
                url: "/api/" + this.root + "/getDataTable",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    quizz.table.setData(response.data);
                    console.log(response.data)
                },
                failure: function (response) {
                    alert("Content load failed: error internal code");
                },
                error: function (response) {
                    alert("Content load failed:  error in ajax petition");
                }
            });
        },
        init: function () {
            $("#Error").hide();
            quizz.userInteraction();
            quizz.getQuizz(1);
            quizz.getDataTable();
        }
    };
    quizz.init();
});
