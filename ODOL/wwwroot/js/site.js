// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// jQuery
$(document).ready(function () {
    GetMahasiswa();
    GetPerusahaan();
    GetAccPeru();
    GetAccPemb();
    /*GetPembimbing();*/
    /*GetMhsPeru();*/

});

function GetAccPeru() {
    $.ajax({
        type: "GET",
        url: "/Pengguna/GetPerusahaan",
        success: function (data) {
            //put to table tpemb

            //if path Mahasiswa then show data
            if (window.location.pathname.split('/')[1] == "Perusahaan") {
                PutAccPeru(data);
            }

        }

    });
}

function GetAccPemb() {
    $.ajax({
        type: "GET",
        url: "/Pengguna/GetPembimbing",
        success: function (data) {
            //put to table tpemb

            //if path Mahasiswa then show data
            if (window.location.pathname.split('/')[1] == "Pembimbing") {
                
                PutAccPem(data);
                
            }

        }
    });
}

function GetPembimbing() {
    var path = window.location.pathname.split('/');
    console.log(path[3]);
    if (path[1] == "Perusahaan" && path[2] == "Detail") {

        $.ajax({
            type: "GET",
            url: "/Pembimbing/GetAllPembimbing/"+path[3],
            success: function (data) {
                //put to table tpemb

                //if path Mahasiswa then show data
                    PutPemb(data);

                

            }

        });
    }

}

function GetMhsPeru() {
    var path = window.location.pathname.split('/');
    console.log(path[3]);
    if (path[1] == "Perusahaan" && path[2] == "Detail") {

        $.ajax({
            type: "GET",
            url: "/Mahasiswa/GetMahasiswaByPeru/" + path[3],
            success: function (data) {
                //put to table tpemb
                console.log(data);
                //if path Mahasiswa then show data
                PutTableMhs(data);
                

            }

        });
    }
}

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

function PutAccPeru(data) {
    for (var i = 0; i < data.data.length; i++) {
        ddlacc.options[ddlacc.options.length] = new Option(data.data[i].nama, data.data[i].id);
    }
}

function PutAccPem(data) {
    for (var i = 0; i < data.data.length; i++) {
        ddlaccpem.options[ddlaccpem.options.length] = new Option(data.data[i].nama, data.data[i].id);
    }
}

function PutTableMhs(data) {
    var tmhs = document.getElementById("myTable1");
    for (var i = 0; i < data.data.length; i++) {
        var row = tmhs.insertRow(i + 1);
        row.insertCell(0).innerHTML = i + 1;
        row.insertCell(1).innerHTML = data.data[i].namaMahasiswa;
        row.insertCell(2).innerHTML = data.data[i].prodi;
        row.insertCell(3).innerHTML = data.data[i].namaPerusahaan;
        //button action
        var btn = document.createElement("a");
        btn.innerHTML = "Detail";
        btn.setAttribute("class", "btn btn-primary");
        btn.setAttribute("href", "/Mahasiswa/Detail?nim=" + data.data[i].nim);




        row.insertCell(4).innerHTML = btn.outerHTML;
    }

}

function PutPemb(data) {
    var tpemb = document.getElementById("TablePemb");
    for (var i = 0; i < data.data.length; i++) {
        var row = tpemb.insertRow(i + 1);
        row.insertCell(0).innerHTML = i + 1;
        row.insertCell(1).innerHTML = data.data[i].namaPembimbing;
        row.insertCell(2).innerHTML = data.data[i].jabatan;
        row.insertCell(3).innerHTML = data.data[i].emailPembimbing;
        //button action
        var btn = document.createElement("a");
        btn.innerHTML = "Ubah";
        btn.setAttribute("class", "btn btn-warning");
        btn.setAttribute("href", "/Pembimbing/Ubah/" + data.data[i].id);
        var btn2 = document.createElement("a");
        btn2.innerHTML = "Hapus";
        btn2.setAttribute("class", "btn btn-danger");
        btn2.setAttribute("id", "btn-delete");
        
        //open modal
        btn2.setAttribute("data-id", data.data[i].id);
        btn2.setAttribute("style", "margin-left:5px;color:white;");
        btn2.setAttribute("data-toggle", "modal");
        btn2.setAttribute("data-target", "#modalHapus");
        btn2.setAttribute("onclick", "document.getElementById('idHapus').value = " + data.data[i].id);
        
        
        
        row.insertCell(4).innerHTML = btn.outerHTML+ "" + btn2.outerHTML;
    
       

    }

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
