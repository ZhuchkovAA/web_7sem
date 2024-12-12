<?php 

namespace app\models;

use yii;
use yii\db\ActiveRecord;

/** 
 * @var int $id
 * @var string $name 
 * 
 * @var Todo $todo
*/

class Category extends ActiveRecord 
{
    public function getTodo()
    {
        return $this->hasMany(Todo::class, ['category_id' => 'id']);
    }
}

?>