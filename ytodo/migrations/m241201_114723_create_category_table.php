<?php

use yii\db\Migration;

/**
 * Handles the creation of table `{{%category}}`.
 */
class m241201_114723_create_category_table extends Migration
{
    /**
     * {@inheritdoc}
     */
    public function safeUp()
    {
        $this->createTable('{{%category}}', [
            'id' => $this->primaryKey(),
            'name'=> $this->string(255)->notNull(),
        ]);

         // Insert default categories
         $this->batchInsert('{{%category}}', ['name'], [
            ['Category 1'],
            ['Category 2'],
            ['Category 3'],
        ]);
    }

    /**
     * {@inheritdoc}
     */
    public function safeDown()
    {
        $this->dropTable('{{%category}}');
    }
}
