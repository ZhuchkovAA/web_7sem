<?php 

namespace app\models;

use yii;
use yii\db\ActiveRecord;


class Category extends ActiveRecord 
{
    public function getTodo()
    {
        return $this->hasMany(Todo::class, ['category_id' => 'id']);
    }
}

?>