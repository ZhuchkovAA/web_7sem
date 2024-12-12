<?php

namespace app\models;

use Yii;
use app\models\Category;
use yii\db\ActiveRecord;

/**
 * @var int $id
 * @var string $name
 * @var int $categoryId
 * @var date $createdAt
 * @var date $updatedAt
 * @var bool $isActive
 * 
 * @var Category $Category
 */


class Todo extends ActiveRecord
{
    public static function tableName()
    {
        return '{{%todo}}';
    }

    public function getCategory()
    {
        return $this->hasOne(Category::class, ['id' => 'category_id']);
    }

    public function getIsActive()
    {
        return $this->is_active;
    }

    public function setIsActive($value)
    {
        $this->is_active = (bool) $value;
    }

}
?>