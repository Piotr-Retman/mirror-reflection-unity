using System;

public class FieldCoords
{
    private int coordX;
    private int coordY;

	public FieldCoords(int x, int y)
	{
        this.coordX = x;
        this.coordY = y;
	}

    public int getCoordX()
    {
        return coordX;
    }

    public int getCoordY()
    {
        return coordY;
    }
}
