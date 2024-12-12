<?php

use yii\helpers\Html;

/**
 * @var yii\web\View $this
 * @var app\models\Category[] $categories
 * @var app\models\Todo[] $todos
 * */
$this->registerJsFile('/js/todo/index.js');
$this->registerCssFile('/css/todo/style.css');
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
                            <?php foreach ($categories as $category): ?>
                                <option value="<?= $category->id ?>"><?= $category->name ?></option>
                            <?php endforeach ?>
                        </select>
                    </div>
                    <div class="col-auto">
                        <input type="name" class="form-control" id="name" placeholder="Задача" required>
                    </div>
                    <div class="col-auto">
                        <button type="submit" class="btn btn-primary mb-3 addbtn">Добавить</button>
                    </div>
                </form>
            </div>
            <div class="col">
                <select id="category-filter" class="form-select">
                    <option value="">Все категории</option>
                    <?php foreach ($categories as $category): ?>
                        <option value="<?= $category->id ?>" 
                                <?= Yii::$app->request->get('category') == $category->id ? 'selected' : '' ?>>
                            <?= $category->name ?>
                        </option>
                    <?php endforeach; ?>
                </select>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <h2>Лист</h2>
                <table class="table" id="tbl-todo">
                    <thead>
                        <tr>
                            <th scope="col">Todo</th>
                            <th scope="col">Категория</th>
                            <th scope="col">Время</th>
                            <th scope="col">Действия</th>
                        </tr>
                    </thead>
                    <tbody class="table-group-divider" id="tbody">
                        <?php
                        foreach ($todos as $todo): ?>
                            <tr class='trow'>
                                <td style="<?= $todo->isActive == 0 ? 'text-decoration: line-through;' : '' ?>"><?= $todo->name ?></td>
                                <td><?= $todo->category ? $todo->category->name : 'No category' ?></td>
                                <td><?= $todo->created_at ?></td>
                                <td class="block block-actions">
                                    <button type="button" class="btn btn-primary edit-btn" data-bs-toggle="modal" data-bs-target="#exampleModal" data-id="<?= $todo->id ?>" data-name="<?= $todo->name ?>" data-category="<?= $todo->category ? $todo->category->id : 'default_value' ?>">Редактировать</button>
                                    <button class="btn btn-danger delete-btn" data-id="<?= $todo->id ?>">Удалить</button>
                                    <input type="checkbox" 
                                        class="input is-active-input" 
                                        data-id="<?= $todo->id ?>" 
                                        <?= !$todo->isActive ? 'checked' : '' ?>
                                    />
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
                <h5 class="modal-title" id="exampleModalLabel">Редактировать</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <form id="editTodoForm">
                    <div class="mb-3">
                        <label for="category" class="form-label">Категория</label>
                        <select class="form-select" id="editCategory" required>
                            <?php foreach ($categories as $category): ?>
                                <option value="<?= $category->id ?>"><?= $category->name ?></option>
                            <?php endforeach ?>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label for="name" class="form-label">Todo</label>
                        <input type="text" class="form-control" id="editName" required>
                    </div>
                </form>
            </div>
            <div class="modal-footer">
                <input type="hidden" name="id" id="id">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Закрыть</button>
                <button type="button" class="btn btn-primary update-btn" id="saveEdit">Сохранить</button>
            </div>
        </div>
    </div>
</div>
