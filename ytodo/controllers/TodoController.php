<?php
namespace app\controllers;

use yii;
use app\models\Todo;
use app\models\Category;
use yii\web\Controller;
use yii\web\Response;


class TodoController extends Controller 
{
    public function actionIndex()
    {
        $categories = Category::find()->all();
        $todos = Todo::find()->all();
        return $this->render('home', ['todos' => $todos, 'categories' =>$categories]);
    }

    public function actionCreate()
    {
        Yii::$app->response->format = Response::FORMAT_JSON;

        $categoryId = Yii::$app->request->post('cat_id');
        $todoName = Yii::$app->request->post('name');

        $category = Category::findOne($categoryId);

        if ($category && $todoName) {
            $todo = new Todo();
            $todo->category_id = $categoryId;
            $todo->name = $todoName;

            if ($todo->save()) {
                return [
                    'status' => 'success',
                    'catname' => $category->name,
                    'todoname' => $todo->name,
                ];
            } else {
                return [
                    'status' => 'error',
                    'message' => 'Failed to save.',
                ];
            }
        } else {
            return [
                'status' => 'error',
                'message' => 'Invalid details.',
            ];
        }
    
    }

    public function actionDelete()
    {
        Yii::$app->response->format = Response::FORMAT_JSON; 

        $todo_Id = Yii::$app->request->post('id');
        $todo = Todo::findOne($todo_Id); 

    if ($todo) {
        if ($todo->delete()) {
            
            return [
                'status' => 'success',
                'message' => 'Todo item deleted successfully.'
            ];
        } else {
            
            return [
                'status' => 'error',
                'message' => 'Failed to delete the todo item.'
            ];
        }
    } else {
        return [
            'status' => 'error',
            'message' => 'Todo item not found.'
        ];
    }
    }

    public function actionUpdate()
    {
        Yii::$app->response->format = Response::FORMAT_JSON;

        $todoId = Yii::$app->request->post('id'); 
        $todoName = Yii::$app->request->post('name'); 
        $categoryId = Yii::$app->request->post('category_id');

        $todo = Todo::findOne($todoId);

        $todo->name = $todoName;
        $todo->category_id = $categoryId;

        if ($todo->save()) {
            return [
                'status' => 'success',
                'message' => 'Todo item updated.',
                'todoname' => $todo->name,
                'category_name' => $todo->category->name,
                'categoryId' => $categoryId,
            ];
        } else {
            return [
                'status' => 'error',
                'message' => 'Failed to update the todo item.',
                'errors' => 'something went wrong'
            ];
        }
    }

}
