#include <avr/io.h>
#include <util/delay.h>

#define BUTTON_CIRCLE 0b00000001
#define BUTTON_EIGHT 0b00000010
#define BUTTON_PAIR_DOWN 0b00000100
#define BUTTON_PAIR_UP 0b00001000

#define LENGTH(arr) (sizeof(arr) / sizeof((arr)[0]))



char circle_pattern[] = { 0b11111110, 0b11111101, 0b11110111, 0b11011111, 0b01111111, 0b10111111, 0b11101111, 0b11111011 };
char eight_pattern[] = { 0b11111110, 0b11111101, 0b11110111, 0b11111011, 0b11101111, 0b10111111, 0b01111111, 0b11011111 };
char pair_pattern[] = { 0b11111100, 0b11110011, 0b11001111, 0b00111111 };


char detect_buttons_in_delay()
{
    static char previous_port = 0xFF;
    char current_port = PINA;
    char buttons = ~current_port & previous_port;
    previous_port = current_port;
    
    if (buttons)
        return buttons;
    return 0x00;
}

void reverse_array(char arr[], int length)
{
    for (int i = 0; i < length / 2; i++)
    {
        char temp = arr[i];
        arr[i] = arr[length - i - 1];
        arr[length - i - 1] = temp;
    }
}

void copy_arr(char prev_arr, char current_arr, int length)
{
    for (int idx)
}

void light_pattern(char arr[])
{
    int length = LENGTH(arr);

    for (int idx = 0; idx < length; idx++)
    {
        PORTD = arr[idx];
        _delay_ms(200);
    }
}

int main() {
    DDRA = 0x00;      // Порт A на ввод
    PORTA = 0xFF;     // Включаем подтягивающие резисторы на порте A
    DDRD = 0xFF;      // Порт D на вывод
    PORTD = 0xFF;     // Изначально все светодиоды выключены

    while (1)
    {
        char buttons;
        do {
            buttons = detect_buttons_in_delay();
            _delay_ms(10);
        } while (!buttons);

        if (buttons & BUTTON_CIRCLE)
        {
            light_pattern(circle_pattern);
            continue;
        }

        if (buttons & BUTTON_EIGHT)
        {
            light_pattern(eight_pattern);
            continue;
        }

        if (buttons & BUTTON_PAIR_DOWN)
        {
            light_pattern(pair_pattern);
            continue;
        }

        if (buttons & BUTTON_PAIR_UP)
        {
            char reverse_pair_pattern[] = pair_pattern;
            reverse_array(reverse_pair_pattern, LENGTH(pair_pattern));
            light_pattern(reverse_pair_pattern);
            continue;
        }
    }

    return 0;
}