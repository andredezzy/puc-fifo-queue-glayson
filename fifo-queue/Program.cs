using System;

namespace fifoqueue
{
    class Program
    {
       private static FilaFIFO filaFIFO = new FilaFIFO();

        static void Main(string[] args)
        {
            // Informações básicas/iniciais da fila
            Console.WriteLine("\n -> CINEMA\n\n");

            Console.WriteLine("Fila: " + (filaFIFO.Vazia() ? "VAZIA" : filaFIFO.GetTamanho().ToString()));
            Console.WriteLine("  Primeiro da fila: " + filaFIFO.GetElementoCabeça().GetElemento());
            Console.WriteLine("  Todas pessoas na fila: \n");

            Console.WriteLine(" ---------->    CABEÇA : " + filaFIFO.GetElementoCabeça().ToString());
            Console.WriteLine(" ---------->    FRENTE : " + filaFIFO.GetElementoFrente().ToString());
            Console.WriteLine(" ---------->    TRAS   : " + filaFIFO.GetElementoTras().ToString());

            // Escolha de opções
            Console.WriteLine("\nOpções:");
            Console.WriteLine("\n  [1] Adicionar uma pessoa ao final da fila");
            Console.WriteLine("  [2] Atender a primeira pessoa da fila\n");

            // Receptor da opção
            Console.Write("> ");
            String option = Console.ReadLine();

            Console.WriteLine("");

            switch (option)
            {
                // Se a opção for: [1] Adicionar
                case "1":
                    Console.Write("Informe o nome da pessoa: ");
                    String inserirElemento = Console.ReadLine();

                    filaFIFO.Enfileira(inserirElemento);

                    break;
                // Se a opção for: [2] Atender
                case "2":
                    Noh nohRemovido = filaFIFO.Desenfileira();

                    if (nohRemovido != null)
                    {
                        Console.WriteLine("[Cinema] " + nohRemovido.GetElemento() + " está sendo atendido.");
                    }

                    break;
            }

            Console.WriteLine("\nPressione ENTER para prosseguir...");
            Console.ReadLine();

            Console.Clear();
            Main(args);
        }
    }

    class FilaFIFO
    {
        private Noh cabeça;
        private Noh frente;
        private Noh tras;
        private int tamanho; // número de elementos na fila

        /**
         * Cria uma fila vazia.
         */
        public FilaFIFO()
        {
            this.cabeça = new Noh("VAZIO");
            this.frente = new Noh("VAZIO");
            this.tras = new Noh("VAZIO");

            this.cabeça.SetProximo(this.frente);
            this.frente.SetProximo(this.tras);

            this.tamanho = 0;
        }


        /*
        * Verifica se a fila está vazia ou não.
        */
        public bool Vazia()
        {
            return this.tamanho == 0;
        }

        public Noh GetElementoFrente()
        {
            return this.frente;
        }

        public Noh GetElementoCabeça()
        {
            return this.cabeça;
        }

        public Noh GetElementoTras()
        {
            return this.tras;
        }

        public int GetTamanho()
        {
            return this.tamanho;
        }

        /**
        * Enfileira um novo elemento na última posição da fila.
        */
        public void Enfileira(Object novoElemento)
        {
            // cria um Noh para o novoElemento
            Noh novoNoh = new Noh(novoElemento);
            // auxiliar para o this.cabeça
            Noh aux = this.cabeça;

            // se a fila estiver vazia
            if (this.Vazia())
            {
                // substitui o this.cabeça pelo novoNoh
                this.cabeça = novoNoh;
                // seta o proximo de this.cabeça como o proximo de aux (antigo this.cabeça) 
                this.cabeça.SetProximo(aux.GetProximo());
            }
            else
            {
                // se houver 3 ou mais elementos na fila
                if (this.tamanho >= 3)
                {
                    // itera por toda a cadeia de elementos
                    for (int i = 0; i <= this.tamanho; i++)
                    {
                        // se o proximo de aux nao for nulo
                        if (aux.GetProximo() != null)
                            // seta aux como o proximo dele
                            aux = aux.GetProximo();
                    }

                    // seta o proximo de aux como novoNoh
                    aux.SetProximo(novoNoh);
                }
                else
                {
                    // itera por toda a cadeia de elementos
                    for (int i = 0; i <= this.tamanho; i++)
                    {
                        // se o proximo de aux for nulo OU o elemento do mesmo for VAZIO
                        if (aux.GetProximo() == null || aux.GetProximo().GetElemento().Equals("VAZIO"))
                        {
                            // para o loop; para a iteração
                            break;
                        }
                        else
                        {
                            // se o proximo do proximo de aux for nulo
                            if (aux.GetProximo().GetProximo() == null)
                                // seta o proximo de novoNoh como o proximo de aux
                                novoNoh.SetProximo(aux.GetProximo());
                        }

                        aux = aux.GetProximo();
                    }

                    aux.SetProximo(novoNoh);
                }
            }

            this.frente = this.cabeça.GetProximo();
            this.tras = this.frente.GetProximo();

            if (this.tras == null) {
                this.tras = new Noh("VAZIO");
                this.frente.SetProximo(this.tras);
            }

            this.tamanho++;
        }

        public Noh Desenfileira()
        {
            Noh removido = this.GetElementoCabeça();

            // se esta fila estiver vazia então indica erro
            if (this.Vazia())
            {
                Console.WriteLine("[Cinema] A fila está vazia!");

                return null;
            }
            else
            {
                if (this.tamanho >= 1)
                {
                    this.cabeça = this.cabeça.GetProximo() != null ? this.cabeça.GetProximo() : new Noh("VAZIO");

                    if (this.tamanho >= 2)
                        this.frente = this.frente.GetProximo() != null ? this.frente.GetProximo() : new Noh("VAZIO");
                    if (this.tamanho >= 3)
                        this.tras = this.tras.GetProximo() != null ? this.tras.GetProximo() : new Noh("VAZIO");

                    this.tamanho--;
                }
            }

            return removido;
        }
    }

    class Noh
    {
        private Object elemento;
        private Noh próximo;

        public Noh(Object elemento)
        {
            this.elemento = elemento;
        }

        public Object GetElemento()
        {
            return this.elemento;
        }

        public void SetElemento(Object novoElemento)
        {
            this.elemento = novoElemento;
        }

        public Noh GetProximo()
        {
            return this.próximo;
        }

        public void SetProximo(Noh próximo)
        {
            this.próximo = próximo;
        }

        public String ToString()
        {
            return "{" + this.elemento + ", 'próximo': " + (this.próximo != null ? this.próximo.ToString() : "null") + "}";
        }
    }
}
