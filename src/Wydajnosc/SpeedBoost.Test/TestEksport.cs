using NUnit.Framework;

namespace SpeedBoost.Test
{
    class TestEksport : MyTestBase
    {
        [Test]
        public void Eksport()
        {
            string result = new EksportPrzelewowBad(Context).Eksportuj();
            Assert.AreEqual(EXPECTED, result);
        }

        const string EXPECTED =
            "1234567890;ABC Sp. z o.o;123,33 PLN;Przelew 0\n" +
            "1234567890;ABC Sp. z o.o;123,33 PLN;Przelew 1\n" +
            "1234567890;ABC Sp. z o.o;123,33 PLN;Przelew 2\n" +
            "1234567890;ABC Sp. z o.o;123,33 PLN;Przelew 3\n" +
            "1234567890;ABC Sp. z o.o;123,33 PLN;Przelew 4\n" +
            "1234567890;ABC Sp. z o.o;123,33 PLN;Przelew 5\n" +
            "1234567890;ABC Sp. z o.o;123,33 PLN;Przelew 6\n" +
            "1234567890;ABC Sp. z o.o;123,33 PLN;Przelew 7\n" +
            "1234567890;ABC Sp. z o.o;123,33 PLN;Przelew 8\n" +
            "1234567890;ABC Sp. z o.o;123,33 PLN;Przelew 9\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 10\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 11\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 12\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 13\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 14\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 15\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 16\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 17\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 18\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 19\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 20\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 21\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 22\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 23\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 24\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 25\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 26\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 27\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 28\n" +
            "1234567890;Warsztat Samochodowy DRYNDA Wiesław Goluch;123,33 PLN;Przelew 29\n";
    }
}
