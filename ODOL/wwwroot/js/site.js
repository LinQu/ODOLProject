// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// jQuery

//$('textarea').on('keydown', function (e) {
//    var t = $(this);
//    switch (e.which) {
//        case 13:
//            t.val(t.val() + '\n•');
//            return false;
//    }
//});

let previousLength = 0;

const handleInput = (event) => {
    const bullet = "\u2022";
    const newLength = event.target.value.length;
    const characterCode = event.target.value.substr(-1).charCodeAt(0);

    if (newLength > previousLength) {
        if (characterCode === 10) {
            event.target.value = `${event.target.value}${bullet} `;
        } else if (newLength === 1) {
            event.target.value = `${bullet} ${event.target.value}`;
        }
    }

    previousLength = newLength;
}

$(function () {
    $('#toggle_status').change(function () {
        var nim = document.getElementById("NIM").value;
        if ($(this).prop('checked')) {

            Swal.fire({
                title: 'Yakin ingin mengubah status menjadi Aktif?',
                showDenyButton: true,
                showCancelButton: false,
                confirmButtonText: 'Yakin',
                denyButtonText: `Batal`,
            }).then((result) => {
                /* Read more about isConfirmed, isDenied below */
                if (result.isConfirmed) {
                    postStatus(nim, "Aktif");
                } else if (result.isDenied) {
                    $(this).prop('checked', false).change();
                    Swal.fire({
                        icon: "info",
                        title: "Status batal diubah",
                        timer: 1500,
                    });
                }
            })
        } else {

            Swal.fire({
                title: 'Yakin ingin mengubah status menjadi Tidak Aktif?',
                showDenyButton: true,
                showCancelButton: false,
                confirmButtonText: 'Yakin',
                denyButtonText: `Batal`,
            }).then((result) => {
                /* Read more about isConfirmed, isDenied below */
                if (result.isConfirmed) {
                    postStatus(nim, "Tidak Aktif");
                } else if (result.isDenied) {
                    $(this).prop('checked', true).change();
                    Swal.fire({
                        icon: "info",
                        title: "Status batal diubah",
                        timer: 1500,
                    });

                }
                
            })
        }
    })
});


$(document).ready(function () {
    //GetMahasiswa();
    GetPerusahaan();
    GetAccPeru();
    GetAccPemb();
    /*GetPembimbing();*/
    /*GetMhsPeru();*/
});

function postStatus(nim, status) {

    var form = new FormData();
    form.append("status", status);
    form.append("nim", nim);

    $.ajax({
        method: "POST",
        url: "/Mahasiswa/UpdateStatus",
        data: form,
        processData: false,
        mimeType: "multipart/form-data",
        contentType: false,
        data: form,
        success: function (data) {
            console.log(data);
            console.log("berhasil dek");
        },
        error: function (data) {
            console.log(data.responseText);
        }
    });
}


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
            url: "/Pembimbing/GetAllPembimbing/" + path[3],
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


function GetPembimbingById() {
    var search = document.getElementById("PembimbingSearch").value;
    $.ajax({
        type: "GET",
        url: "/Pembimbing/GettPembimbingById/" + search,
        success: function (data) {
            console.log(data.data.jabatan);
            $("#JabatanPemb").val(data.data.jabatan);
        }
    });
}

function GetMahasiswa() {
    $.ajax({
        type: "GET",
        url: "/Mahasiswa/GetAllMahasiswa1",
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
        url: "/Mahasiswa/SearchMhsApi",

        data: "nim=" + NIM,
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



        row.insertCell(4).innerHTML = btn.outerHTML + "" + btn2.outerHTML;



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

    NIM.value = data.data[0].nim;
    Nama.value = data.data[0].nama;
    prodi.value = data.data[0].prodi;


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

function test() {

}

function PrintPDF(id) {

    $.ajax({
        type: "GET",
        url: "/Penilaian/GetNilai/"+id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data);
            console.log(data.nilai);
            console.log(data.rata);
            
            var docDefinition = {
                info: {
                    title: 'Penilaian ' + data.periode + ' - ' + data.nim + '',
                },
                content: [
                    {
                        text: 'Rincian Penilaian ' + data.nilai.periode + '',
                        style: 'header',
                        alignment: 'center',
                        margin: [0, -20, 0, 20]
                    },

                    {
                        layout: 'noBorders',
                        margin: [0, 0, 0, 10],
                        table: {
                            body: [
                                [{ text: 'NIM' }, { text: ':' }, { text: data.nim }],
                                [{ text: 'Nama' }, { text: ':' }, { text: data.nama }],
                                [{ text: 'Prodi' }, { text: ':' }, { text: data.prodi }],
                                [{ text: 'Dinilai Oleh' }, { text: ':' }, { text: data.pembimbing }],
                            ]
                        }
                    },
                    {
                        table: {
                            headerRows: 1,
                            widths: [25, 90, 330, 'auto'],
                            heights: [
                                'auto',
                                'auto',
                                'auto',
                                'auto',
                                'auto',
                                20,
                                20, 20, 5,
                            ],
                            body: [
                                [{ text: 'No', style: 'tableHeader' }, { text: 'Penilaian', style: 'tableHeader' }, { text: 'Kriteria Penilaian', style: 'tableHeader' }, { text: 'Nilai', style: 'tableHeader' }],
                                [{ text: '1', style: 'no' }, { text: 'Penilaian Diri', style: 'no' }, { text: '• KEMAMPUAN MENGANALISA TUGAS \n • PENGETAHUAN SECARA UMUM SECARA BIDANG TERKAIT \n• KECEPETAN MENERIMA BIMBINGAN \n • DAYA TANGKAP TERHADAP HAL-HAL BARU', style: 'value' }, { text: data.nilai.pengetahuanKerja, margin: [8, 20, 8, 30], }],
                                [{ text: '2', margin: [0, 15, 0, 15], alignment: 'center', }, { text: 'Kualitas Kerja', margin: [0, 15, 0, 15], alignment: 'center', }, { text: '• HASIL KERJA (PRODUK/JASA) \n • KEMAMPUAN MELAKUKAN ANALISIS DAN PENYELESAIAN MASALAH \n• KEMAMPUAN MENYELESAIKAN TUGAS ', style: 'value' }, { text: data.nilai.kualitasKertja, margin: [8, 15, 8, 15], }],
                                [{ text: '3', style: 'no' }, { text: 'Kecepatan Kerja', style: 'no' }, { text: '• KECEPATAN MENYELESAIKAN TUGAS \n • KECEPATAN MAMAHAMI TUGAS DARI MENTOR \n• KEMAMPUAN MELAKUKAN ANALISIS DAN PENYELESAIAN MASALAH ', margin: [0, 10, 0, 10], fontSize: 10, }, { text: data.nilai.kecepatanKerja, margin: [8, 20, 8, 20], }],
                                [{ text: '4', margin: [0, 20, 0, 10], alignment: 'center', }, { text: 'Sikap & Perilaku', margin: [0, 20, 0, 20], alignment: 'center', }, { text: '• TAAT PERATURAN PERUSAHAAN \n • MENGGUNAKAN PERLENGKAPAN STANDAR KERJA DAN MEMPERHATIKAN SAFETY \n• CUSTOMER SATISFACTION (KEPUASAN PELANGGAN) ', style: 'value' }, { text: data.nilai.sikapPerilaku, margin: [8, 20, 8, 20], }],
                                [{ text: '5', margin: [0, 20, 0, 15], alignment: 'center', }, { text: 'Kreativitas dan Kerja Sama', margin: [0, 15, 0, 15], alignment: 'center', }, { text: '• MEMBERIKAN IDE/SARAN DALAM PROSES KERJA ATAU  MELAKUKAN IMPROVEMENT \n • KERJA SAMA TIM DALAM LINGKUNGAN KERJA ', margin: [0, 15, 0, 15], fontSize: 10, }, { text: data.nilai.kreatifitasKerjaSama, margin: [8, 20, 8, 15], }],
                                [{ text: '6', rowSpan: 3, style: 'no1' }, { text: 'Softskill', rowSpan: 3, style: 'no1' }, { text: 'Leadership', margin: [0, 8, 0, 8], fontSize: 10, }, { text: data.nilai.leadership, margin: [8, 8, 8, 8], }],
                                [{ text: '', style: 'no' }, '', { text: 'Kemampuan Menangani Masalah', margin: [0, 8, 0, 8], fontSize: 10, }, { text: data.nilai.penangananMasalah, margin: [8, 8, 8, 8], }],
                                [{ text: '', style: 'no' }, '', { text: 'Kemampuan Beradapatasi', margin: [0, 8, 0, 8], fontSize: 10, }, { text: data.nilai.beradaptasi, margin: [8, 8, 8, 8], }],
                                [{ text: 'Rata Rata', colSpan: 3, margin: [0, 10, 0, 10], alignment: 'center', }, { text: '' }, { text: '', margin: [0, 0, 0, 0] }, { text: data.rata.rata, margin: [8, 10, 8, 10], }],
                            ]
                        }
                    },
                    {
                        text: 'Komentar dari Pembimbing : \n ' + data.nilai.ulasan + '',
                        margin: [0, 20, 0, 0],
                    }
                ],

                styles: {

                    header: {
                        fontSize: 20,
                        bold: true,
                        margin: [0, 0, 0, 0],

                    },
                    tableHeader: {
                        fontWeight: 'bold',
                        alignment: 'center',

                    },
                    no: {
                        margin: [0, 20, 0, 30],
                        alignment: 'center',

                    },
                    no1: {
                        margin: [0, 40, 0, 40],
                        alignment: 'center',

                    },
                    value: {
                        margin: [0, 5, 0, 5],
                        fontSize: 10,
                    }

                }
            };

            pdfMake.createPdf(docDefinition).open();
        }
    });
}

