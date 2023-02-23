// Phần appen
$("#btnAdd").click(function () {
    // lấy id của tr cuối cùng thuộc thẻ table có class = ChiTietphieuNhap
    // Bước 4: Phương thức find là tìm đến thẻ nào đó: ở đây là thẻ tr (:last-child) là thẻ cuối cùng trong thẻ tblChiTietPhieuNhap
    var id_cuoi = $(".ChiTietPhieuNhap").find("tr:last-child").attr("data-id");
    i = parseInt(id_cuoi) + 1;
    // Bước 1:Nội dung phía trong thẻ trAppend
    var tdnoidung = $(".trAppend").html();
    // Bước 2: Tạo 1 thẻ tr bao ngoài nội dung
    var trnoidung = "<tr class=\"trAppended\" data-id=\"" + i + "\">" + tdnoidung + "</tr>";
    // Bước 3: Lấy thẻ table append vào 1 tr
    $(".ChiTietPhieuNhap").append(trnoidung);
    loadIDLENTHE();
});

// Phương thức sử lý lấy thuộc tính attr từ thẻ tr gắn xuống chỉ xố phần tử các trong thuộc tính name của thẻ input
function loadIDLENTHE() {
    $(".trAppended").each(function () {
        var id = $(this).attr("data-id");
        //var nameMaPhieuNhap = "[" + id + "].MaSach";
        var nameMaHoaChat = "[" + id + "].MAHC"; // tạo ra mã hoá chất
        var nameSoLuongNhap = "[" + id + "].SOLUONGNHAP"; // tạo ra số lượng nhập
        //var nameNoiDung = "[" + id + "].NOIDUNG"; // tạo ra nội dung nhập
        $(this).find(".ddlHoaChat").attr("name", nameMaHoaChat); // gán name cho dropdowlist
        $(this).find(".txtSoLuongNhap").attr("name", nameSoLuongNhap); // gan so luong nhap sach
    //    $(this).find(".txtNoiDung").attr("name", nameNoiDung); // gan so luong nhap sach
    });
}
// sử lý sự kiện xóa 1 dòng từ nút delete nằm bên trong thẻ tr
$("body").on("click", ".btnDelete", function () {
    // Xóa phần tử cha phía ngoài
    $(this).closest(".trAppended").remove();
    CapNhatID();
});

function CapNhatID() {
    //Lấy lại tr 1
    var id_cuoi = $(".ChiTietPhieuNhap").find(".trFirstChild").attr("data-id");
    i = parseInt(id_cuoi) + 1;

    $(".trAppended").each(function () {
        var id = i;
        $(this).attr("data-id", i)
        // cập nhật lại id (tr) khi xóa
        var nameMaHoaChat = "[" + id + "].MAHC"; // tạo ra mã sp
        var nameSoLuongNhap = "[" + id + "].SOLUONGNHAP"; // tạo ra số lượng nhập
        var nameNoiDung = "[" + id + "].NoiDung"; // tạo ra nội dung nhập
        $(this).find(".ddlHoaChat").attr("name", nameMaHoaChat); // gán name cho dropdowlist
        $(this).find(".txtSoLuongNhap").attr("name", nameSoLuongNhap); // gan so luong nhap sach
        //$(this).find(".txtNoiDung").attr("name", nameNoiDung); // gan so luong nhap sach
        i++;
    });
}
$("#btnNhapHang").click(function () {
    if (KiemTraSoLuong() == false) {
        return false;
    }
    //if (KiemtraNoiDung() == false) {
    //    return false;
    //}
})
// kiểm tra Số lượng
function KiemTraSoLuong() {
    var bl = true;
    // duyệt vòng lặp each
    $(".txtSoLuongNhap").each(function () {
        var giatri = $(this).val();
        if (parseFloat(giatri) < 0) {
            alert("Số lượng nhập lớn hơn 0");
            bl = false;
            return bl;
        }
        if (isNaN(giatri) == true) {
            alert("Số lượng nhập không hợp lệ !");
            bl = false;
            return bl;
        }
    });
    return bl;
}
//// kiểm tra nội dung
//function KiemtraNoiDung() {
//    var bl = true;
//    //duyệt vòng lặp each
//    $(".txtNoiDung").each(function () {
//        var giatri = $(this).val();
//        if (parseFloat(giatri) < 0) {
//            alert("Nội dung nhập phải lớn hơn 0");
//            bl = false;
//            return bl;
//        }
//        if (isNaN(giatri) == true) {
//            alert("Nội dung nhập không hợp lệ");
//            bl = false;
//            return bl;
//        }
//    });
//    return bl;
//}
