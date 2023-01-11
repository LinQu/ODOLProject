// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// jQuery
$(document).ready(function () {
    GetMahasiswa();
    GetPerusahaan();
    
});

function GetPerusahaan() {
    var perusahaan = document.getElementById("perusahaan");
    $.ajax({
        type: "GET",
        url: "/Perusahaan/GetAllPerusahaan",
        success: function (data) {
            //console.log(data.data[1]);
            //if path Mahasiswa then show data
            if (window.location.pathname == "/Mahasiswa/Tambah") {
                PutPeru(data);
            }
            
        }
    });
}

function GetMahasiswa() {
    var ddlmhs = document.getElementById("ddlmhs");
    $.ajax({
        type: "GET",
        url: "/Mahasiswa/GetAllMahasiswa",
        success: function (data) {
            //console.log(data.data[1]);
            //if path Mahasiswa then show data
            if (window.location.pathname == "/Mahasiswa/Tambah") {
                PutMhs(data);
            }

        }
    });
}


function GetMhs() {
    var NIM = document.getElementById("ddlmhs").value;
    $.ajax({
        type: "GET",
        url: "/Mahasiswa/SearchMhs",
        data: "search=" + NIM,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            OnSucces(data);
        }
    });
}

function GetPeru() {
    var Peru = document.getElementById("perusahaan").value;
    $.ajax({
        type: "GET",
        url: "/Perusahaan/SearchPeru",
        data: "search=" + Peru,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data);
            PutDataPeru(data);
        }
    });
}

function PutDataPeru(data) {
    var Alamat = document.getElementById("alamat");
    var Cabang = document.getElementById("cabang");
    var Group = document.getElementById("group");

    Alamat.value = data.data[0].alamatPerusahaan;
    Cabang.value = data.data[0].cabang;
    Group.value = data.data[0].group;
    
}

function OnSucces(data) {
    var NIM = document.getElementById("NIM");
    var Nama = document.getElementById("Nama");
    var prodi = document.getElementById("prodi");

    NIM.value = data[0].nim;
    Nama.value = data[0].nama;
    prodi.value = data[0].prodi;


}

function PutPeru(data) {
    for (var i = 0; i < data.data.length; i++) {
        perusahaan.options[perusahaan.options.length] = new Option(data.data[i].namaPerusahaan, data.data[i].id);
    }
}

function PutMhs(data) {

    for (var i = 0; i < data.data.length; i++) {
        ddlmhs.options[ddlmhs.options.length] = new Option(data.data[i].nama, data.data[i].nim);
    }
}
