<?php
use yii\helpers\Html;
/** @var yii\web\View $this */

$this->title = 'My To Do App';
?>
<div class="site-index">

    <div class="jumbotron text-center bg-transparent mt-5 mb-5">
        <h1 class="display-4">To Do List Application!</h1>

        <p class="lead">Where to do items are added/deleted and belong to category.</p>

        <p>Create To-do's</p>
    </div>

    <div class="body-content">

        <div class="row">
            <div class="col">
                <h2>List of To-do's</h2>

                <table class="table">
  <thead>
    <tr>
      <th scope="col">#</th>
      <th scope="col">Todo Item Name</th>
      <th scope="col">Category</th>
      <th scope="col">Timestamp</th>
      <th scope="col">Actions</th>
    </tr>
  </thead>
  <tbody class="table-group-divider">
    <tr>
      <th scope="row">1</th>
      <td></td>
      <td></td>
      <td></td>
    </tr>
    
  </tbody>
</table>
                
                

                <p><a class="btn btn-outline-secondary" href="https://www.yiiframework.com/doc/">Yii Documentation &raquo;</a></p>
            </div>
            
        </div>

    </div>
</div>
