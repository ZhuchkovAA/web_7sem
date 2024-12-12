$(document).ready(function () {
    $('.addbtn').click(function () {
        event.preventDefault();
        var cat_id = $('#category').val();
        var todo_name = $('#name').val();

        if (todo_name != "") {
            $.ajax({
                url: '/todo/create',
                type: 'POST',
                data: {
                    cat_id: cat_id,
                    name: todo_name,
                },
                success: function (response) {
                    if (response.status === 'success') {
                        location.reload()
                    }
                },
            });
        }
    });


    $('#tbl-todo').on('click', '.edit-btn', function () {
        var todoId = $(this).data('id');
        var todoName = $(this).data('name');
        var categoryId = $(this).data('category');

        $('#id').val(todoId);
        $('#editName').val(todoName);
        $('#editCategory').val(categoryId);

        $('.update-btn').click(function () {
            var updatedName = $('#editName').val();
            var updatedCategory = $('#editCategory').val();
            $.ajax({
                url: '/todo/update',
                type: 'POST',
                data: {
                    id: todoId,
                    name: updatedName,
                    category_id: updatedCategory
                },
                success: function (response) {
                    if (response.status === 'success') {
                        location.reload()
                        alert(response.message);
                    }
                }
            });
        });

    });

    $('#tbl-todo').on('click', '.delete-btn', function () {
        var todoId = $(this).data('id');
        $.ajax({
            url: '/todo/delete',
            type: 'POST',
            data: {
                id: todoId,
            },
            success: function (response) {
                if (response.status === 'success') {
                    $('button[data-id="' + todoId + '"]').closest('tr').remove();
                }
            }
        })
    });

    $('#tbl-todo').on('click', '.is-active-input', function () {
        var todoId = $(this).data('id');
        $.ajax({
            url: '/todo/change-active',
            type: 'POST',
            data: {
                id: todoId
            },
            success: function (response) {
                if (response.status === 'success') {
                    location.reload()
                }
            }
        });
    })

    $('#category-filter').on('change', function() {
        const categoryId = $(this).val();
        window.location.href = '/todo/index' + (categoryId ? `?category=${categoryId}` : '');
    });
    
});