function btnEtkinlikSil(clicked_id) {
    id = clicked_id;
    swal({
        title: "Onaylama",
        text: "Etkinlik Silinecektir",
        type: "warning",
        showCancelButton: true, 
        confirmButtonText: "Evet", 
        cancelButtonText: "Hayır", 
        showLoaderOnConfirm: true 
    }, function (isConfirm) {
        if (isConfirm) 
        {
            SilmeIslemi(id);
        }
        else {
            toastr.warning("Silme işlemi İptal Edildi!");
        }
    });
}

function SilmeIslemi(id) {
    setTimeout(function () {
        $.ajax({
            method: 'POST',
            data: { pId: id },
            url: '/Etkinlikler/EtkinlikSil',
            beforeSend: function () {

            }
        }).done(function (veri) {
            console.log(veri);
            if (veri.basarili) {
                SilmeBasarili(veri);
            }
            else {
                SilmeBasarisiz(veri.mesaj);
            }

        }).fail(function () {

        }).always(function () {

        });
    }, 1000);
}
function SilmeBasarili(veri) {
    setTimeout(function () {
        swal({
            title: "Uyarı",
            text: "Silme işlemi Başarıyla Gerçekleşti!",
            type: "warning",
            showCancelButton: false,
            confirmButtonText: "Onayla",
            showLoaderOnConfirm: true
        }, function (isConfirm) {
            location.reload();

        });
    }, 1000);
}
function SilmeBasarisiz(mesaj) {
    swal({
        title: "Silme İşlemi",
        text: mesaj,
        type: "warning"
    });
    toastr.warning("İşlem Başarısız!");
}