using System;
using IniParser;
using IniParser.Model;
using Alba.CsConsoleFormat;
using static System.ConsoleColor;

namespace calculo_punteria
{
	class Program
	{
		static void Main()
		{			
			var parser_clases = new FileIniDataParser();
			IniData clases = parser_clases.ReadFile("clases.ini");

			var parser_razas = new FileIniDataParser();
			IniData razas = parser_razas.ReadFile("razas.ini");

			bool correr = true;
					   
			float mod_cca = 0;
			float mod_pro = 0;
			float mod_csa = 0;

			float agip = 0;

			float modc = 0;

			double punteria;

			while (correr == true)
			{

				// Calculo de punteria en base a inputs.
				string valido = "no";
				while (valido == "no")
				{
					Console.WriteLine("Cual es la clase del personaje que ataca?");
					string claseInput = Console.ReadLine().ToUpper();
					try
					{
						mod_cca = float.Parse(clases[claseInput]["CCA"]);

						mod_pro = float.Parse(clases[claseInput]["PRO"]);

						mod_csa = float.Parse(clases[claseInput]["CSA"]);

						valido = "si";
					}
					catch
					{
						Console.WriteLine("No es una clase valida.");
					}
				}

				valido = "no";
				while (valido == "no")
				{
					Console.WriteLine("Cual es la raza del personaje que ataca?");
					String razaInput = Console.ReadLine().ToUpper();
					try
					{
						agip = (18 + float.Parse(razas[razaInput]["AGI"])) * 2;
						if (agip > 40)
							agip = 40;

						valido = "si";
					}
					catch
					{
						Console.WriteLine("No es una raza valida.");
					}
				}

				valido = "no";
				while (valido == "no")
				{
					Console.WriteLine("Cual es el tipo de combate en uso? (Combate con Armas = 1, Proyectiles = 2, Combate sin Armas = 3)");
					String tipo = Console.ReadLine();

					if (tipo == "1")
					{
						modc = mod_cca;

						valido = "si";
					}

					else if (tipo == "2")
					{
						modc = mod_pro;

						valido = "si";
					}

					else if (tipo == "3")
					{
						modc = mod_csa;

						valido = "si";
					}

					else
						Console.WriteLine("Debe ingresar el tipo de combate.");
				}

				punteria = (100 + agip * 3) * modc + 70;

				// Funcion que calcula evasion y chance de pegar.
				string chances(float agi, float modt, float mode)
				{
					double evasion = (100 + 100 / 33.0 * agi) * modt + 70 + 100 * mode / 2;
					double chance = 50 + 0.4 * (punteria - evasion);
					if (chance < 10)
						chance = 10;
					if (chance > 100)
						chance = 100;

					string chance_con_formato = String.Format("{0:0.00}", chance);
					string chance_con_porcentaje = (chance_con_formato + "%");

					return chance_con_porcentaje;
				}

				// Genera array de todas las clases.
				int i = 0;
				string[] clases_array = new string[int.Parse(clases["CLASES"]["CANTIDAD"])];

				valido = "no";
				while (valido == "no")
				{
					try
					{
						clases_array[i] = clases["CLASES"][i.ToString()];
						i += 1;
					}

					catch
					{
						valido = "si";
					}
				}

				// Genera array de todas las razas.
				i = 0;
				string[] razas_array = new string[int.Parse(razas["RAZAS"]["CANTIDAD"])];

				valido = "no";
				while (valido == "no")
				{
					try
					{
						razas_array[i] = razas["RAZAS"][i.ToString()];
						i += 1;
					}

					catch
					{
						valido = "si";
					}
				}

				// Usa la funcion con las distintas combinaciones de clase y raza y crea un array con las chances de golpe encabezado por la clase.

				int combinaciones = int.Parse(razas["RAZAS"]["CANTIDAD"]) * int.Parse(clases["CLASES"]["CANTIDAD"]);

				i = 0;
				string[] chances_array = new string[combinaciones];

				foreach (string clase in clases_array)
				{
					float modt = float.Parse(clases[clase]["TDC"]);
					float mode = float.Parse(clases[clase]["ESC"]);

					foreach (string raza in razas_array)
					{
						float agi = (18 + float.Parse(razas[raza]["AGI"])) * 2;
						if (agi > 40)
							agi = 40;

						string funcion = chances(agi, modt, mode);

						chances_array[i] = funcion;
						i += 1;
					}
				}

				// Crea una tabla en base a la cantidad de clases, la cantidad de razas y las distintas combinaciones de chance.

				var doc = new Document(
				);

				Grid tabla = new Grid
				{
					Color = Blue,
					Columns = { },
					Children = { new Cell(), }
				};

				tabla.Columns.Add(GridLength.Auto);
				
				foreach (string n in razas_array)
				{
					tabla.Columns.Add(GridLength.Auto); ;
				}

				foreach (string n in razas_array)
				{
					tabla.Children.Add(new Cell(n) { Color = Yellow });
				}

				i = 0;
				int index = 0;

				foreach (string clase in clases_array)
				{
					tabla.Children.Add(new Cell(clase) { Color = Red });
					while (i < int.Parse(razas["RAZAS"]["CANTIDAD"]))
					{
						tabla.Children.Add(new Cell(chances_array[index]) { Color = Gray });
						i += 1;
						index += 1;
					}
					i = 0;
				}

				doc.Children.Add(tabla);
				ConsoleRenderer.RenderDocument(doc);

				// Loopea el codigo o lo cierra.
				valido = "no";
				while (valido == "no")
				{
					Console.WriteLine("Correr nuevamente? (Si/No)");
					string correr_nuevamente = Console.ReadLine().ToUpper();

					if (correr_nuevamente == "SI")
					{
						correr = true;
						valido = "si";
					}

					else if (correr_nuevamente == "NO")
					{
						correr = false;
						valido = "si";
					}

					else						
						Console.WriteLine("Debes ingresar si o no.");
				}
			}
		}
	}
}


