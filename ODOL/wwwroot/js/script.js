//ketika pertama kali buka page jalankan function
$(window).on("resize", function () {
	//cek ukuran layar
	// console.log($(window).width());
	if ($(window).width() > 1000) {
		changestate("open");
	}
});

//datatable
$(document).ready(function () {
	$('#myTable').DataTable({
		"paging": true,
		"lengthChange": false,
		"searching": true,
		"ordering": true,
		"info": true,
		"autoWidth": false,
		"responsive": true,
		"pageLength": 5,
		"language": {
			"emptyTable": "Data Masih Kosong"
		},
		//"dom": 'B<"clear">lfrtip',
		//"buttons": {
		//	name: 'primary',
		//	buttons: ['copy', 'csv', 'excel']
		//}
	});
	$('#myTableLogBookBelum').DataTable({
		"paging": true,
		"lengthChange": false,
		"searching": true,
		"ordering": true,
		"info": true,
		"autoWidth": false,
		"responsive": true,
		"pageLength": 5,
		"language": {
			"emptyTable": "Data Masih Kosong"
		},
		"dom": 'Bfrtip',
		"buttons": [
			{
				"text": 'Isi LogBook',
				"className": 'btn-primary',
				"init": function (api, node, config) {
					$(node).removeClass('btn-secondary');
				},
				"action": function (e, dt, node, config) {
                    window.location.href = "/LogBook/Tambah";
                    
				},
                
			}
		]
	});

	$('#myTableLogBookSudah').DataTable({
		"paging": true,
		"lengthChange": false,
		"searching": true,
		"ordering": true,
		"info": true,
		"autoWidth": false,
		"responsive": true,
		"pageLength": 5,
		"language": {
			"emptyTable": "Data Masih Kosong"
		},
		
	});

	$('#myTableRiwayat').DataTable({
		"paging": true,
		"lengthChange": false,
		"searching": true,
		"ordering": true,
		"info": true,
		"autoWidth": false,
		"responsive": true,
		"pageLength": 5,
		"language": {
			"emptyTable": "Data Masih Kosong"
		},
		"dom": 'B<"clear">lfrtip',
		
	});

	$('#myTable1').DataTable({
		"paging": true,
		"lengthChange": false,
		"searching": true,
		"ordering": true,
		"info": true,
		"autoWidth": false,
		"responsive": true,
		"pageLength": 5,
	});

	$('#TablePemb').DataTable({
		"paging": true,
		"lengthChange": false,
		"searching": true,
		"ordering": true,
		"info": true,
		"autoWidth": false,
		"responsive": true,
		"pageLength": 5,
	});

});


//ketika url Dashboard menu class active
$(document).ready(function () {
	//cek url
	const url = window.location.href;
	
	console.log(url);
    //get tittle page
    
	const title = window.document.title;
	console.log(title);
	console.log(title == "Home Page - ODOL");
	

	if (url.indexOf("Home") > -1) {
		$("#menuberanda").addClass("active");
		$("#menuberanda").removeClass("hovermenu");
		$("#menuberanda").removeClass("text-dark");
		console.log("warna ilang");
	} else if (url.indexOf("Mahasiswa") > -1) {
		$("#menumahasiswa").addClass("active");
		$("#menumahasiswa").removeClass("hovermenu");
        $("#menumahasiswa").removeClass("text-dark");
	} else if (url.indexOf("Perusahaan") > -1) {
		$("#menuperusahaan").addClass("active");
		$("#menuperusahaan").removeClass("hovermenu");
        $("#menuperusahaan").removeClass("text-dark");
	} else if (url.indexOf("Pengguna") > -1) {
		$("#menuuser").addClass("active");
		$("#menuuser").removeClass("hovermenu");
		$("#menuuser").removeClass("text-dark");
	} else if (url.indexOf("Cabang") > -1) {
		$("#menucabang").addClass("active");
		$("#menucabang").removeClass("hovermenu");
        $("#menucabang").removeClass("text-dark");
	} else if (url.indexOf("LogBook") > -1) {
		$("#menulogbook").addClass("active");
		$("#menulogbook").removeClass("hovermenu");
		$("#menulogbook").removeClass("text-dark");
	} else if (url.indexOf("Profile") > -1) {
		$("#menuprofile").addClass("active");
		$("#menuprofile").removeClass("hovermenu");
		$("#menuprofile").removeClass("text-dark");
	} 
	else if (url.indexOf("Penilaian") > -1) {
		$("#menunilai").addClass("active");
		$("#menunilai").removeClass("hovermenu");
		$("#menunilai").removeClass("text-dark");
	} 
	else {
		$("#menuberanda").addClass("active");
		$("#menuberanda").removeClass("hovermenu");
		$("#menuberanda").removeClass("text-dark");
	}
});

function changestate(param) {
	if (param === "open") {
		$(".panelmenu").css("display", "block");
		$("#menuOpen").css({
			cursor: "pointer",
			display: "none",
		});
		$("#menuClose").css({
			cursor: "pointer",
			display: "initial",
		});
	} else {
		$(".panelmenu").css("display", "none");
		$("#menuOpen").css({
			cursor: "pointer",
			display: "initial",
		});
		$("#menuClose").css({
			cursor: "pointer",
			display: "none",
		});
	}
}


$('.btn-delete').on('click', function () {
	// get data from button delete
	const id = $(this).data('id');
	// Set data to Form Edit
	$('.id').val(id);
	// Call Modal Edit
	$('#modalHapus').modal('show');
});





function validatepass() {
    var pass = document.getElementById("pass").value;
	var konfirpass = document.getElementById("konfirpass").value;
	var alert = document.getElementById("alert");
	if (pass != konfirpass) {
		alert.classList.add("alert-danger");
        alert.classList.remove("alert-success");
		alert.innerHTML = "Password tidak sama";
		alert.style.display = "block";

    } else {
		alert.classList.add("alert-success");
		alert.classList.remove("alert-danger");
		alert.innerHTML = "Password sama";
    }
	return true;
}

function validatepass1() {
	var pass = document.getElementById("pass1").value;
	var konfirpass = document.getElementById("konfirpass1").value;
	var alert = document.getElementById("alert");
	if (pass != konfirpass) {
		alert.classList.add("alert-danger");
		alert.classList.remove("alert-success");
		alert.innerHTML = "Password tidak sama";
		alert.style.display = "block";

	} else {
		alert.classList.add("alert-success");
		alert.classList.remove("alert-danger");
		alert.innerHTML = "Password sama";
	}
	return true;
}

function callSwal(id,status) {
	Swal.fire({
		title: "Silahkan Mengisikan Catatan Untuk Mahasiswa",
		input: "textarea",
		inputPlaceholder: "Silakan Mengisi dengan benar,Boleh Kosong",
		showCancelButton: true,
		cancelButtonText: 'Batal',
		confirmButtonText: "Simpan ",
		inputValidator: function (value) { // validates your input
			return new Promise(function (resolve, reject) {
				
					// document.getElementById('closeComment').value = value; // assign this to other elements in your form
					$.ajax({
						type: "POST",
						url: "/LogBook/UpdateStatus/",
						data: {
							"id": id,
							"ulasan": value,
                            "status" : status
						},
						success: function (data) {
							if (data == "success") {
								Swal.fire({
									title: "Berhasil",
									text: "Catatan Berhasil Disimpan",
									icon: "success",
									confirmButtonText: "OK"
								}).then(function () {
									location.reload();
								});
							}
						},
						error: function (data) {
                            console.log(data);
						}
					});
                    
					Swal.fire({
						title: "Berhasil",
                        text: "Catatan Berhasil Disimpan",
						icon: "success",
						confirmButtonText: "OK"
					}).then(function () {
						location.reload();
					});
                    
					// call other functions or do an AJAX call or sumbit your form
			
			});
		}
	});
}