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