﻿var datatable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/Place/GetAll"
        },
        "columns": [
            { "data": "name", "width": "70%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="text-center">
                       <a href="/Admin/Place/Upsert/${data}" class="btn btn-info">
                        <i class="fas fa-edit"></i></a>
                        <a class="btn btn-danger" onclick=Delete('/Admin/Place/Delete/${data}')>
                        <i class="fas fa-trash-alt"></i></a>
                    </div>
                    `;
                }
                }        ]
        })
}
function Delete(url) {
    swal({
        title: "Want To Delete Data?",
        text: "Delete Information!",
        icon: "warning",
        buttons: true,
        dangerModel:true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }

                }
                })
        }
    })

}