var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable(){
    $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getallitems' },
        "columns": [
            { data: 'title', "width": "10%" },
            {data : 'description', "width": "15%"},
            { data: 'isbn', "width": "10%"},
            { data: 'authurName', "width": "10%"},
            { data: 'listPrice', "width": "8%"},
            {data : 'price50', "width": "8%"},
            {data : 'price', "width": "8%"},
            {data : 'price100', "width": "8%"},
            { data: 'category.name', "width": "5%" },
            {
                data: 'id',
                "render": function (data) {
                    return ` <div class="w-75 btn-group" role="group">
                        <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-1" ><i class="bi bi-pencil-square"></i></a>
                        <a onClick=Delete('/admin/product/deletes/${data}') class="btn btn-danger mx-1" ><i class="bi bi-trash-fill"></i></a>
                    </div>`
                },
                "width": "3%"
            } 
        ]
    })

} function Delete(url) {
    Swal.fire({
        title: `are you sure ${url} `,
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}

