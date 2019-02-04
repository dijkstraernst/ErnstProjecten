<?php
/**
 * Created by PhpStorm.
 * User: Ernst
 * Date: 24-1-2019
 * Time: 12:45
 */

$image='download.png';
$im = imagecreatefrompng($image);   //$image is een PNG
$count = 0;
$str = "";
$lastfound = 0;
$morse = "";

for($y=0;$y<imagesy($im);$y++)
{
    for($x=0;$x<imagesx($im);$x++)
    {
        if(imagecolorat ($im,$x,$y))    //Get Color index codes
        {
            $str .= chr(($count-$lastfound));
            $lastfound = $count;
        }
        $count++;
    }
}

echo "[+]String: $str \n";
$arr = explode(' ',$str);

foreach($arr as $i)
{
    $morse .= translateMorse($i);   //Voor elke color code index lees welke het is tot er geen meer gevonden kan worden
}

echo "[+]Translated from morse: $morse\n";

function translateMorse($code)
{
    switch ($code) {
        case '.-':
            return 'a';
        case '-...':
            return 'b';
        case '-.-.':
            return 'c';
        case '-..':
            return 'd';
        case '.':
            return 'e';
        case '..-.':
            return 'f';
        case '--.':
            return 'g';
        case '....':
            return 'h';
        case '..':
            return 'i';
        case '.---':
            return 'j';
        case '-.-':
            return 'k';
        case '.-..':
            return 'l';
        case '--':
            return 'm';
        case '-.':
            return 'n';
        case '---':
            return 'o';
        case '.--.':
            return 'p';
        case '--.-':
            return 'q';
        case '.-.':
            return 'r';
        case '...':
            return 's';
        case '-':
            return 't';
        case '..-':
            return 'u';
        case '...-':
            return 'v';
        case '.--':
            return 'w';
        case '-..-':
            return 'x';
        case '-.--':
            return 'y';
        case '--..':
            return 'z';
        case '-----' :
            return '0';
        case '.----':
            return '1';
        case '..---' :
            return '2';
        case '...--' :
            return '3';
        case '....-' :
            return '4';
        case '.....' :
            return '5';
        case '-....' :
            return '6';
        case '--...' :
            return '7';
        case '---..' :
            return '8';
        case '----.' :
            return '9';
        case '.-.-.-' :
            return '.';
        case '--..--' :
            return ',';
        case '..--..' :
            return '?';
        case '-..-.' :
            return '/';
        case ' ' :
            return ' ';
            default:
            return false;
    }
}

