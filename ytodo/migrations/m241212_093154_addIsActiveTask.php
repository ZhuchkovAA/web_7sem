<?php

use yii\db\Migration;

/**
 * Class m241212_093154_addIsActiveTask
 */
class m241212_093154_addIsActiveTask extends Migration
{
    /**
     * {@inheritdoc}
     */
    public function safeUp()
    {
        $this->addColumn('{{%todo}}', 'is_active', $this->integer()->notNull()->defaultValue(1));
    }

    /**
     * {@inheritdoc}
     */
    public function safeDown()
    {
        echo "m241212_093154_addIsActiveTask cannot be reverted.\n";

        return false;
    }

    /*
    // Use up()/down() to run migration code without a transaction.
    public function up()
    {

    }

    public function down()
    {
        echo "m241212_093154_addIsActiveTask cannot be reverted.\n";

        return false;
    }
    */
}
