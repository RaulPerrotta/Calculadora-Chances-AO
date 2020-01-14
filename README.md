# Calculadora-Chances-AO
El programa genera una tabla que muestra las chances de golpe de una clase y raza especifica vs. todas las combinaciones de raza y 
clase del juegp. Estas chances se calculan con todas las combinaciones de raza y clase teniendo el mismo nivel, su agilidad maxeada 
y la cantidad de skills en sus habilidades siempre en 100.

Para definir al personaje que ataca se toman 3 inputs. Estos son clase, raza y tipo de ataque.
El listado de clases validas se puede encontrar en clases.ini, el listado de razas validas se puede encontrar en razas.ini, y el tipo
de ataque se define ingresando 1, 2 o 3 segun que modificador de combate (Combate con Armas, Armas de Proyectiles y Combate sin Armas 
respectivamente) se quiere usar.

Es posible cambiar los modificadores de clase y raza entrando a sus respectivos archivos .ini.

Los distintos modificadores de clase en clases.ini son;
[CLASE]
CCA = Modificador de punteria en Combate con Armas.
PRO = Modificador de punteria en Armas de Proyectiles.
CSA = Modificador de punteria en Combate sin Armas.
TDC = Modificador de evasion en Tacticas de Combate.
ESC = Modificador de evasion en Defensa con Escudos.

Los distintos modificadores de raza en razas.ini son;
[RAZA]
FUE = Modificador en el atributo Fuerza.
AGI = Modificador en el atributo Agilidad.
INT = Modificador en el atributo Inteligencia.
CAR = Modificador en el atributo Carisma.
CON = Modificador en el atributo Constitucion.

Para agregar nuevas clases o razas se debe utilizar el formato antes mencionado asi como actualizar el listado de clases o razas que
se encuentra en el inicio de cada .ini.
