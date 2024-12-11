<?php
use yii\helpers\Html;
/** @var yii\web\View $this */

$this->title = 'My To Do App';
?>
<div class="site-index">

    <div class="jumbotron text-center bg-transparent mt-5 mb-5">
        <h1 class="display-4">To Do List App</h1>
    </div>
    <div class="body-content">
    <div class="row">
      <div class="col">
        <form class="row g-3">
          <div class="col-auto">
            <select class="form-select" id="category" aria-label="Default select example" required>
              <?php foreach($categories as $category): ?>
                <option value="<?= $category->id ?>"><?= $category->name ?></option>
                <?php endforeach ?>
            </select>
          </div>
          <div class="col-auto">
            <input type="name" class="form-control" id="name" placeholder="Type here" required>
          </div>
          <div class="col-auto">
            <button type="submit" class="btn btn-primary mb-3 addbtn">Add</button>
          </div>
        </form>
        </div>
    </div>
    <div class="row">
        <div class="col">
            <h2>List of To-do's</h2>
            <table class="table" id="tbl-todo">
              <thead>
              <tr>
                <th scope="col">Todo Item Name</th>
                <th scope="col">Category</th>
                <th scope="col">Timestamp</th>
                <th scope="col">Actions</th>
              </tr>
              </thead>
              <tbody class="table-group-divider" id="tbody">
              <?php
              foreach($todos as  $todo): ?>
              <tr id='trow'>
                
                <td><?= $todo->name ?></td>
                <td><?= $todo->category ? $todo->category->name : 'No category' ?></td>
                <td><?= $todo->created_at ?></td>
                <td>
                  <button type="button" class="btn btn-primary edit-btn" data-bs-toggle="modal" data-bs-target="#exampleModal" data-id="<?= $todo->id ?>" data-name="<?= $todo->name ?>" data-category="<?= $todo->category ? $todo->category->id : 'default_value' ?>">Edit</button>
                  <button class="btn btn-danger delete-btn" data-id="<?= $todo->id ?>">Delete</button>
                </td>
              </tr> 
              <?php 
               
              endforeach; ?>

              </tbody>
            </table>
          
            
        </div>
        
    </div>

    </div>
</div>

<!-- Modal -->
<div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Edit Todo</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <form id="editTodoForm">
          <div class="mb-3">
            <label for="category" class="form-label">Category</label>
            <select class="form-select" id="editCategory" required>
            <?php foreach($categories as $category): ?>
                <option value="<?= $category->id ?>"><?= $category->name ?></option>
                <?php endforeach ?>
            </select>
          </div>
          <div class="mb-3">
            <label for="name" class="form-label">Todo Name</label>
            <input type="text" class="form-control" id="editName" required>
          </div>
        </form>
      </div>
      <div class="modal-footer">
        <input type="hidden" name="id" id="id">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary update-btn" id="saveEdit">Save changes</button>
      </div>
    </div>
  </div>
</div>



<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<script>
  $(document).ready(function() {    
    $('.addbtn').click(function (){

      event.preventDefault();     
      var cat_id = $('#category').val();
      var todo_name = $('#name').val();

      if(todo_name != ""){

        $.ajax({
          url: '/todo/create',
          type:'POST',
          data: 
          {
            cat_id: cat_id,
            name: todo_name,
          },
          success: function(response) {         
                    if (response.status === 'success') {
                      let currentTime = new Date().toLocaleString();
                      var newRow = `
                            <tr>
                               
                                <td>${response.todoname}</td>
                                 <td>${response.catname}</td>
                                <td>${currentTime}</td>
                               <td>
                    <button type="button" class="btn btn-primary edit-btn" data-bs-toggle="modal" data-bs-target="#exampleModal" data-id="${response.todoid}" data-name="${response.todoname}" data-category="${response.category_name}">Edit</button>
                    <button class="btn btn-danger delete-btn" data-id="${response.todoid}">Delete</button>
                </td>
                            </tr>
                        `;
                        $('#tbl-todo tbody').append(newRow);
                        $('#name').val('');
                    } else {
                        alert('Error: ' + response.message);
                    }
                }, 
        });
      }else{
        alert('Please enter the todo name first');
      }
    });


    // $('.edit-btn').click(function(){
      $('#tbl-todo').on('click', '.edit-btn', function() {


        var todoId = $(this).data('id');
        var todoName = $(this).data('name');
        var categoryId = $(this).data('category');

        $('#id').val(todoId);
        $('#editName').val(todoName);
        $('#editCategory').val(categoryId);

        $('.update-btn').click(function() {
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
            success: function(response) {
                if (response.status === 'success') {
                    $('#exampleModal').modal('hide');
                    var row = $('button[data-id="' + todoId + '"]').closest('tr');
                    row.find('td:nth-child(1)').text(response.todoname); 
                    row.find('td:nth-child(2)').text(response.category_name);
                    alert(response.message); 
                } else {
                    alert(response.message);
                }
            },
            error: function() {
                alert('An error occurred while updating the todo.');
            }
        });
      });
        
    });
  
    /////
    $('#tbl-todo').on('click', '.delete-btn', function() {

    var todoId = $(this).data('id'); 
    if (confirm('Are you sure?')) {
        $.ajax({
          url: '/todo/delete',
            type: 'POST',
            data: {
                id: todoId,
            },
            success: function(response) {
                if (response.status === 'success') {
                    $('button[data-id="' + todoId + '"]').closest('tr').remove();
                    alert(response.message); 
                } else {
                    alert(response.message);
                }
            },
            error: function() {
                alert('An error occurred while deleting the todo item.');
            }
        });
    }
  });


  });
</script>
