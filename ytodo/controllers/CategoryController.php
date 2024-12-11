<?php
namespace app\controllers;

use yii;
use app\models\Category;
use yii\web\Controller;

class CategoryController extends Controller 
{
    public function actionIndex()
    {
        $categories = Category::find()->all();
        return $this->render('home', ['categories' => $categories]);
    }
}
