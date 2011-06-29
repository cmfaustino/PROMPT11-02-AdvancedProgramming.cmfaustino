using System.Collections;

namespace Mod02_AdvProgramming.Assignments {
    using System;
    using System.Collections.Generic;

    public class Ex3 {
        public class FibonacciSequence : IEnumerable<int>
        {
            //auxiliar nao utilizado
            private int FibSeqN(int numOrdem)
            {
                switch (numOrdem)
                {
                    case 0:
                        {
                            return 0;
                        }
                    case 1:
                        {
                            return 1;
                        }
                    default:
                        {
                            return numOrdem > 0
                                       ? FibSeqN(numOrdem - 2) + FibSeqN(numOrdem - 1)
                                       : FibSeqN(numOrdem + 2) - FibSeqN(numOrdem + 1);
                        }
                }
            }

            //auxiliar utilizado
            private int? limite_interno; // null = sequencia infinita, int = sequencia finita
            private int num_penultimo;
            private int num_ultimo;
            private int num_de_retornados;

            public void FibonacciSequenceYield()
            {
                limite_interno = null;

                num_penultimo = 0;
                num_ultimo = 0;
                num_de_retornados = 0;
            }

            public void FibonacciSequenceYield(int limit)
            {
                limite_interno = limit;

                num_penultimo = 0;
                num_ultimo = 0;
                num_de_retornados = 0;
            }

            public FibonacciSequence()
			{
                //throw new NotImplementedException();
                FibonacciSequenceYield();
			}

            public FibonacciSequence(int limit)
            {
                //throw new NotImplementedException();
                FibonacciSequenceYield(limit);
            }

            #region Implementation of IEnumerable

            public IEnumerator<int> GetEnumeratorYield()
            {
                if (limite_interno == null) // sequencia infinita ,  assume-se como sendo positiva
                {
                    while (true) // faltava ciclo infinito !!!
                    {
                        switch (num_de_retornados)
                        {
                            case 0: // resultado fixo : zero ... antes ainda nao se tinha retornado numeros
                                {
                                    num_penultimo = 0;
                                    num_ultimo = 0;

                                    num_de_retornados++;

                                    yield return 0;
                                    break;
                                }
                            case 1: // resultado fixo : um ... antes so se tinha retornado um numero
                                {
                                    num_penultimo = 0;
                                    num_ultimo = 1;

                                    num_de_retornados++;

                                    yield return 1;
                                    break;
                                }
                            default: // resultado recursivo : soma do penultimo com ultimo
                                {
                                    var soma = num_penultimo + num_ultimo; // temporaria so para guardar valor a retornar

                                    num_penultimo = num_ultimo;
                                    num_ultimo = soma;

                                    num_de_retornados++;

                                    yield return soma;
                                    break;
                                }
                        }
                    }
                }
                else // sequencia finita ,  assumem-se os varios casos positiva e negativa
                {
                    for (int i = 0; i < limite_interno; i++) // assume sequencia positiva
                    {
                        switch (num_de_retornados)
                        {
                            case 0: // resultado fixo : zero ... antes ainda nao se tinha retornado numeros
                                {
                                    num_penultimo = 0;
                                    num_ultimo = 0;

                                    num_de_retornados++;

                                    yield return 0;
                                    break;
                                }
                            case 1: // resultado fixo : um ... antes so se tinha retornado um numero
                                {
                                    num_penultimo = 0;
                                    num_ultimo = 1;

                                    num_de_retornados++;

                                    yield return 1;
                                    break;
                                }
                            default: // resultado recursivo : soma do penultimo com ultimo
                                {
                                    var soma = num_penultimo + num_ultimo; // temporaria so para guardar valor a retornar

                                    num_penultimo = num_ultimo;
                                    num_ultimo = soma;

                                    num_de_retornados++;

                                    yield return soma;
                                    break;
                                }
                        }
                    }
                }
            }

            public IEnumerator<int> GetEnumerator()
            {
                //throw new NotImplementedException();
                return GetEnumeratorYield();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            #endregion
        }
    }
}