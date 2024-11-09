var dataTable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: "/Admin/News/GetAll"
        },
        "columns": [
            {
                "data": "date", "width": "5%",
                "render": function (data) {
                    // Customize the date format here
                    return new Date(data).toLocaleDateString('en-US', { year: 'numeric', month: 'short', day: 'numeric' });
                }
            },
            { "data": "headLine", "width": "5%" },
            { "data": "place.name", "width": "5%" },
            { "data": "category.name", "width": "5%" },
            {
                "data": "id", "width": "5%",
                "render": function (data) {
                    return `
                        <div class="text-center">                  
                            <a href="/Admin/News/Upsert/${data}" class="btn btn-info">
                                <i class="fas fa-edit"></i>
                            </a>
                            <a class="btn btn-danger" onclick=Delete("/Admin/News/Delete/${data}")>
                                <i class="fas fa-trash-alt"></i>
                            </a>
                        </div>
                    `;
                }
            }
        ]
    })
}

function Delete(url) {
    swal({
        title: "Want To Delete Data?",
        text: "Delete Information!",
        icon: "warning",
        buttons: true,
        dangerModel: true
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