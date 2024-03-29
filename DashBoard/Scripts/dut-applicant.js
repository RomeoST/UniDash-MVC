﻿var listChanged = [];
// Edit form active
var isEditable = false;
// Send to server only SAVE
var isNotSendClick = false;

var applicant = {
    ApplicantId: "",
    NameApplicant: "",
    PhoneApplicant: "",
    SchoolCollege: "",
    Address: "",
    MailApplicant: "",
    Speciality: "",

    add: function () {
        if (isEditable === true) return;
        $('#list-applicant').append($('<option/>', { text: 'Новий абітурієнт', value: '-1' }));
        $('#list-applicant option').last().attr('selected', 'selected');
        editableMode(true, true);

        $('#NameApplicant').val('');
        $('#PhoneApplicant').val('');
        $('#MailApplicant').val('');
        $('#ApplicantId').val('');
        $('#selectedListDepart option').each(function () {
            $(this).remove();
        });
    },
    remove: function() {
        $('#appldel').text(applicant.NameApplicant);
        $('#Id').val(applicant.ApplicantId);
        $('#delModal').modal('show');
    },
    save: function(event) {
        var textSpec = '';
        $('#selectedListDepart option').each(function () {
            textSpec += this.value + ' ';
        });
        $('#Speciality').val(textSpec);
    },
    load: function(count) {
        editableMode(false, true);
        $.ajax({
            type: 'GET',
            url: '/Applicant/LoadApplicants?count=' + count,
            success: function (data) {
                $('#list-applicant').replaceWith(data);
                $("#list-applicant").change(listApplicantSelectEvent);
                $('.list-applicant-info').text('| Рядок 0 з ' + $('#list-applicant option').size());
                NProgress.done();
            },
            beforeSend:function() {
                NProgress.start();
            }
        });
    },
    search: function () {
        if ($('#bl-search').children().length) {
            $('#bl-search').empty();
        } else {
            $.ajax({
                type: 'GET',
                url: '/Applicant/GetBlSearch',
                success: function(data) {
                    $('#bl-search').append(data);
                }
            });
        }
    },
    addDepart: function() {
        var comboBox = $("#listDepart option:selected");

        var btmp = false;
        var lDepOption = $("#selectedListDepart option");
        if (lDepOption.length === 4) {
            new PNotify({
                title: 'Попередження',
                text: 'Максимальна кількість побажань - 4',
                type: 'warning',
                styling: 'bootstrap3'
            });
            return;
        }
        lDepOption.each(function () {
            if ($(this).val() === comboBox.val()) {
                btmp = true;
                return true;
            }
        });

        if (btmp) {
            new PNotify({
                title: 'Попередження',
                text: 'Спеціальність вже додана у список',
                type: 'warning',
                styling: 'bootstrap3'
            });
            return;
        }

        $("#selectedListDepart").append($("<option/>",
            {
                value: comboBox.val(),
                text: comboBox.text()
            }));

        $("#selectedListDepart").css('background-color', 'rgb(255, 255, 146)');
        listChanged.push('selectedListDepart');

        editableMode(true, true);
    },
    delDepart: function() {
        var lDepOption = $("#selectedListDepart option:selected");
        if (lDepOption.length === 0) {
            new PNotify({
                title: 'Попередження',
                text: 'Нічого не обрано для видалення',
                type: 'warning',
                styling: 'bootstrap3'
            });
            return;
        }

        lDepOption.each(function () {
            $(this).remove();
        });

        $("#selectedListDepart").css('background-color', 'rgb(255, 255, 146)');
        listChanged.push('selectedListDepart');

        editableMode(true, true);
    },
    cancel: function () {
        editableMode(false, false);

        $('#ApplicantId').val(applicant.ApplicantId);
        $('#NameApplicant').val(applicant.NameApplicant);
        $('#PhoneApplicant').val(applicant.PhoneApplicant.slice(1, applicant.PhoneApplicant.length));
        $('#SchoolCollege').val(applicant.SchoolCollege);
        $('#Address').val(applicant.Address);
        $('#MailApplicant').val(applicant.MailApplicant);

        updateSelectDepart(applicant.Speciality);

        if (applicant.ApplicantId === "") {
            $('#list-applicant option:selected').remove();
        }
    },
    handler: function (data) {
        // save, remove of applicant
        // if data is error
        if (Array.isArray(data)) {
            alert("Обработка ошибок.\n Некоторые данные не заполнены.");
            return;
        }
        // incerepted errors
        if (data.model === 'failed') {
            new PNotify({ title: 'Помилка', text: data.modelList, type: 'error', styling: 'bootstrap3' });
            return;
        }

        if (data.model === 'confirmed') {
            new PNotify({ title: 'Повідомлення', text: data.modelList, type: 'success', styling: 'bootstrap3' });
            editableMode(false);

            if (typeof data.type === 'undefined')
                return;

            switch (data.type) {
            case "save":
                {
                    if (typeof data.applicantId !== 'undefined') {
                        $('#ApplicantId').val(data.applicantId);
                    }
                    
                    $('#list-applicant option:selected').text($('#NameApplicant').val());
                }
                break;
            case "remove":
                {
                    editableMode(false, true);
                    $('#list-applicant option:selected').remove();
                    $('#delModal').modal('hide');
                }
                break;
            default:
            }
        }
    }
};

$("#PhoneApplicant").mask('+380 (99) 999-99-99');

$('#form1').on('change',
    function (e) {
        if (e.target === null || e.target.id === 'listDepart' || e.target.id === 'selectedListDepart') return;
        editableMode(true, true);
        listChanged.push(e.target.id);
        $('#' + e.target.id).css('background-color', 'rgb(255, 255, 146)');
    });

// Init context menu for applicant-list
var menu = new BootstrapMenu('#list-applicant',
{
    actionsGroups: [['add', 'remove'], ['load10', 'load50', 'load100']],
    actions: {
        add:
        {
            name: 'Додати абітурієнта',
            iconClass: 'fa fa-plus',
            onClick: function (row) {
                applicant.add();
            },
            isEnabled: function (row) {
                return !isEditable;
            }
        },
        remove:
        {
            name: 'Видалити абітурієнта',
            iconClass: 'fa fa-minus',
            onClick: function (row) {
                applicant.remove();
            },
            isEnabled: function (row) {
                var sel = $('#list-applicant option:selected').length;
                return !isEditable && sel > 0;
            },
        },
        load10:
        {
            name: 'Завантажити останні 10',
            iconClass: 'fa-circle-o-notch fa-spin',
            onClick: function (row) {
                applicant.load(10);
            }
        },
        load50:
        {
            name: 'Завантажити останні 50',
            iconClass: 'fa-circle-o-notch fa-spin',
            onClick: function (row) {
                applicant.load(50);
            }
        },
        load100:
        {
            name: 'Завантажити останні 100',
            iconClass: 'fa-circle-o-notch fa-spin',
            onClick: function (row) {
                applicant.load(100);
            }
        }
    }
    });

$('#btn-save').click(applicant.save.bind(applicant));
$('#btn-cancel').click(applicant.cancel);
$("#btn-add-depart").click(applicant.addDepart.bind(applicant));
$("#btn-del-depart").click(applicant.delDepart.bind(applicant));
$('#btn-search').click(applicant.search);

// Event select listbox (list-applicant)
function listApplicantSelectEvent() {
    var currentApplicantId = $(this).val();
    $.ajax({
        type: 'GET',
        url: '/Applicant/GetApplicantDetail?id=' + currentApplicantId,
        success: function (obj) {
            if (obj.model === 'failed') {
                new PNotify({ title: 'Результат', text: obj.modelList[0], type: 'error', styling: 'bootstrap3' });
                return;
            }

            editableMode(false, false);

            applicant.ApplicantId = obj.applicant.ApplicantId;
            applicant.NameApplicant = obj.applicant.NameApplicant;
            applicant.PhoneApplicant = obj.applicant.PhoneApplicant;
            applicant.SchoolCollege = obj.applicant.SchoolCollege;
            applicant.Address = obj.applicant.Address;
            applicant.MailApplicant = obj.applicant.MailApplicant;
            applicant.Speciality = obj.applicant.Speciality;

            $('#ApplicantId').val(obj.applicant.ApplicantId);
            $('#NameApplicant').val(obj.applicant.NameApplicant);
            $('#PhoneApplicant').val(obj.applicant.PhoneApplicant.slice(1, obj.applicant.PhoneApplicant.length));
            $("#PhoneApplicant").mask('+380 (99) 999-99-99');   
            $('#SchoolCollege').val(obj.applicant.SchoolCollege);
            $('#Address').val(obj.applicant.Address);
            $('#MailApplicant').val(obj.applicant.MailApplicant);
            $('#DateAdd').val(obj.applicant.DateAdd);
            $('#DateEdit').val(obj.applicant.DateEdit);
            updateSelectDepart(applicant.Speciality);

            $('.list-applicant-info').text('| Рядок ' + ($('#list-applicant option:selected').index() + 1) + ' з ' + $('#list-applicant option').size());
        }
    });
}

function updateSelectDepart(values) {
    $('#selectedListDepart option').each(function () { $(this).remove() });

    var splitdata = values.split(' ');
    var listDepart = $('#listDepart option');

    $.each(splitdata, function (index1, data) {
        listDepart.each(function () {
            if (this.value === data) {
                $('#selectedListDepart').append($('<option/>',
                    {
                        text: this.text,
                        value: this.value
                    }));
            }
        });
    });
}

// Edit mode for form
function editableMode(status, disform) {
    if (status) {
        $('#btn-save').show();
        $('#btn-cancel').show();
        isEditable = true;

        if (disform) {
            $('#form_zero *').prop('disabled', false);
        } else {
            $('#form_zero *').prop('disabled', true);
        }

        $('#list-applicant').prop('disabled', true);
    } else {
        $('#btn-save').hide();
        $('#btn-cancel').hide();

        for (var i = 0; i < listChanged.length; i++) {
            $('#' + listChanged[i]).css('background-color', 'white');
        }

        isEditable = false;

        if (disform)
            $('#form_zero *').prop('disabled', true);
        else {
            $('#form_zero *').prop('disabled', false);
        }

        $('#list-applicant').prop('disabled', false);
    }
}

// before sending to the server (cancel/save)
function applicantHandlerBegin(e, data) {
    if (isNotSendClick) {
        isNotSendClick = false;
        return false;
    }
}