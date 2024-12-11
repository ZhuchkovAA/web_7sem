<?php

namespace app\models;

use app\models\Category;
use Yii;
use yii\db\ActiveRecord;


class Todo extends ActiveRecord
{
    public function getCategory()
    {
        return $this->hasOne(Category::class, ['id' => 'category_id']);
    } 
}
?>