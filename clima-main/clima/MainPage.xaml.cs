using System.Text.Json;

namespace clima;

public partial class MainPage : ContentPage
{
	const string Url="https://api.hgbrasil.com/weather?woeid=455927&key=54f7cbee";
	Resposta resposta;

	public MainPage()
	{
		InitializeComponent();

		AtualizaTempo();
		
	}

		

	void PreencherTela()
	{
		LabelChuva.Text = resposta.results.rain.ToString();
		LabelHumidade.Text = resposta.results.humidity.ToString();
		LabelGraus.Text = resposta.results.temp.ToString();
		LabelClima.Text = resposta.results.description;
		LabelCidade.Text = resposta.results.city;
		LabelForça.Text = resposta.results. wind_speed.ToString();
		LabelDireção.Text = resposta.results.wind_direction.ToString();
		LabelAmanhecer.Text = resposta.results.sunrise;
		LabelAnoitecer.Text = resposta.results.sunset;
		LabelLua.Text = resposta.results.moon_phase;
		LabelCardinal.Text = resposta.results.wind_cardinal;
		LabelNublado.Text = resposta.results.cloudness.ToString();

		if (resposta.results.currently == "dia")
		{
			if (resposta.results.rain >= 35)
				imgBackground.Source = "diachuva.webp";
			else if (resposta.results.cloudness >= 35)
				imgBackground.Source = "dianublado.webp";
			else
				imgBackground.Source = "dia.webp";
		}
		else if (resposta.results.currently == "noite")
		{
			if (resposta.results.rain >= 35)
				imgBackground.Source = "noitechuva.webp";
			else if (resposta.results.cloudness >= 35)
				imgBackground.Source = "noitenublada.webp";
			else
				imgBackground.Source = "noite.webp";
		}

	}

	async void AtualizaTempo()
	{
		try
		{
			var httpClient = new HttpClient();
			var response  = await httpClient.GetAsync(Url);
			if (response.IsSuccessStatusCode)
			{
				string content = await response.Content.ReadAsStringAsync();
				resposta = JsonSerializer.Deserialize<Resposta>(content);
			}
		}
		catch(Exception e)
		{
			//Erro
			
		}

		PreencherTela();
	}
		
}

