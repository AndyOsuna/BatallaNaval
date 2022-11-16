# BatallaNaval
Trabajo práctico N°4 para Programación II

* Interfaces *  -> FRONT 
- Hay que hacer dos forms aparte del de login y register
    - Form de main menu
          . Este form tiene tres opciones: - Jugar solo -> Instancia un objeto tipo Board 
                                           - Jugar VS IA -> Instacia dos objetos tipo Board e instancia un objeto Tipo IA
                                           - Modificar tablero -> Hace set a las dos propiedades de tamaño de battleship 
          . A las Board se les pasa por parametro filas y columnas del tablero, que se hacen con un get de las propiedades de battleship
            
    - Form de Juego 
          - Este form es una matriz de botones si jugas solo o una matriz de botones y una de tu tablero si jugas vs la IA
          - Cuando apretas un boton para disparar haces board.Shoot(x,y) - Siendo x e y las coordenadas de la posicion del boton
            Instantaneamente refrescas la pantalla para actualizar los datos
          - El estado de los botones lo sacas de la matriz board, que es un parametro del objeto Board 
                En la matriz tenemos:
                    - Un espacio vacio donde no hay nada
                    - Una O para donde hay una parte del barco 
                    - Una X donde un barco es golpeado 
                    - Una a donde el disparo da al agua 
        - Si se juega conra la IA tu tablero es un Board tambien, solo que este es afectado en vez de por botones, por el metodo de la IA . -> IA.CaculateShoot(Objeto Board)
           - El metodo devuelve una coordenadas donde la IA pega, por lo que haces Board.Shoot(Coordenadas)
                                         
