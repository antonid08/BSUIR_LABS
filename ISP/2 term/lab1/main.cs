using System;

class Question
{
	string text;
	string explanation;

	int answer;

	public void set_text(string text) {
		this.text = text;
	}

	public string get_text(){
		return this.text;
	}

	public void set_answer(int answer) {
		this.answer = answer;
	}

	public int get_answer(){
		return answer;
	}

	public void set_explanation(string explanation) {
		this.explanation = explanation;
	}

	public string get_explanation() {
		return explanation;
	}
}

class TheGame  
{	

	public static void Main (string[] args)
	{	
		const int MAX_SCORE = 300;
		const int MIN_SCORE = 0;
		const int DIFF_SCORE = 20;
		const int QUANTITY_OF_QUESTIONS = 15;

		int current_score = 140;
		int number_of_question = 0;
		int player_answer;

		Random rnd = new Random();

		Question[] question = new Question[QUANTITY_OF_QUESTIONS];

		for (int counter = 0; counter < QUANTITY_OF_QUESTIONS; counter++) {
			question[counter] = new Question();
		}

		question[number_of_question].set_text("Из улья вылетели три пчелы. Вы уверены, что через 15 секунд они будут находиться в одной плоскости?");
		question[number_of_question].set_answer('y');
		question[number_of_question].set_explanation("Да. Три точки всегда образуют плоскость.");

		number_of_question++;
		question[number_of_question].set_text("Конструкторы тепловозов стремятся уменьшить их вес?");
		question[number_of_question].set_answer('n');
		question[number_of_question].set_explanation("Нет. Вес нужен для лучшего сцепления с рельсами.");

		number_of_question++;
		question[number_of_question].set_text("Две монеты в сумме дают 3 рубля. Одна из них - не 1 рубль. Есть ли монеты номиналом в 1 рубль?");
		question[number_of_question].set_answer('y');
		question[number_of_question].set_explanation("Да. Монеты: 2 рубля и 1 рубль. Одна из них - не рубль, но вторая - рубль.");

		number_of_question++;
		question[number_of_question].set_text("Правда ли, что куры могут глотать пищу только в вертикальном положении?");
		question[number_of_question].set_answer('y');
		question[number_of_question].set_explanation("Да. У кур пища опускается в желудок под действием силы тяжести, а не с помощью мышц.");

		number_of_question++;
		question[number_of_question].set_text("У каждого человека индивидуальный отпечаток языка?");
		question[number_of_question].set_answer('y');
		question[number_of_question].set_explanation("Да.");

		number_of_question++;
		question[number_of_question].set_text("Может ли заяц весной забежать глубже середины леса?");
		question[number_of_question].set_answer('n');
		question[number_of_question].set_explanation("Нет. Заяц может добежать только до середины, дальше он уже выбегает из леса.");

		number_of_question++;
		question[number_of_question].set_text("Как утверждают японские ученые, чтобы мозг не старился быстро, нужно думать поменьше?");
		question[number_of_question].set_answer('n');
		question[number_of_question].set_explanation("Нет. Наоборот.");

		number_of_question++;
		question[number_of_question].set_text("можно ли из двух химических элементов создать еще один элемент?");
		question[number_of_question].set_answer('y');
		question[number_of_question].set_explanation("Да. Гальванический.");

		number_of_question++;
		question[number_of_question].set_text("В россии было два генералиссимуса: Суворов и Сталин?");
		question[number_of_question].set_answer('n');
		question[number_of_question].set_explanation("Нет. Их было пять.");

		number_of_question++;
		question[number_of_question].set_text("Пчелы в жаркую погоду пьянеют?");
		question[number_of_question].set_answer('y');
		question[number_of_question].set_explanation("Да. От перебродившего нектара.");

		number_of_question++;
		question[number_of_question].set_text("Существуют ли четырехногие животные, у которых все четыре ноги одинаково функциональны?");
		question[number_of_question].set_answer('y');
		question[number_of_question].set_explanation("Да. Слоны.");

		number_of_question++;
		question[number_of_question].set_text("Чарли Чаплин известен не только фильмами, но еще и своей музыкой?");
		question[number_of_question].set_answer('y');
		question[number_of_question].set_explanation("Да. К некоторым фильмам он сам писал музыку");

		number_of_question++;
		question[number_of_question].set_text("Фазан - это та же курица?");
		question[number_of_question].set_answer('y');
		question[number_of_question].set_explanation("Да. Они из одного семейства.");

		number_of_question++;
		question[number_of_question].set_text("Существовали ли файлы mp3 формата в 1993 году?");
		question[number_of_question].set_answer('y');
		question[number_of_question].set_explanation("Да. Формат mp3 был изобретен в 1991 году.");

		number_of_question++;
		question[number_of_question].set_text("В Германии на автострадах еть специальные переходы для лягушек и жаб на случай гололеда?");
		question[number_of_question].set_answer('y');
		question[number_of_question].set_explanation("Да. Они представляют собой трубы полотном дороги.");


		Console.WriteLine("У вас " + current_score + " очков. Отвечайте на вопросы (y - да / n -нет. Для победы требуется " + MAX_SCORE + " очков." );
		Console.WriteLine("Чтобы начать, нажмите любую клавишу.\n");
		Console.Read();	

		do {
			number_of_question = rnd.Next(0, QUANTITY_OF_QUESTIONS - 1);
			Console.WriteLine(question[number_of_question].get_text());
			player_answer = Console.Read();
			while ((player_answer != 'y') && (player_answer != 'n')) {
				Console.WriteLine("\nВведите корректный ответ: ");
				player_answer = Console.Read();
				Console.WriteLine("\n");
			}
			if (player_answer == question[number_of_question].get_answer()){
				current_score += DIFF_SCORE;
			} else {
				current_score -= DIFF_SCORE;
			}
			Console.WriteLine("\n" + question[number_of_question].get_explanation());
			Console.WriteLine("Ваши очки: " + current_score);
			Console.Write("\n\n");
		} while ((current_score < MAX_SCORE) && (current_score > MIN_SCORE));

		if (current_score >= MAX_SCORE){
			Console.WriteLine("Поздравляю! Вы выиграли!");
		} else {
			Console.WriteLine("Увы, вы проиграли :(");
		}

	}
}