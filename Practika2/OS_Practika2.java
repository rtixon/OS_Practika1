import java.nio.charset.StandardCharsets;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Scanner;

public class OS_Practika2 {
    public static void main(String[] args) throws NoSuchAlgorithmException, InterruptedException{
        
        Scanner input = new Scanner(System.in); // Объявляем Scanner
        //System.out.println("\nВведите количество хэш-значений: ");
        //int size = input.nextInt();

        //size = size + 1;// Читаем с клавиатуры размер массива и записываем в size
        int size = 4;
        String array[] = new String[size];
        array[0] = "1115dd800feaacefdf481f1f9070374a2a81e27880f187396db67958b207cbad";
        array[1] = "3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b";
        array[2] = "74e1bb62f8dabb8125a58852b63bdf6eaef667cb56ac7f7cdba6d7305c50a22f";
         // Создаём массив int размером в size
        //System.out.println("\nВведите все элементы по порядку через Engine");
        /*Пройдёмся по всему массиву, заполняя его*/
        //for (int i = 0; i < size; i++) {
        //array[i] = input.nextLine(); // Заполняем массив элементами, введёнными с клавиатуры
    //}
        System.out.print ("\nВведенные хэш-значения: ");
        for (int i = 0; i < (size-1); i++) {
            System.out.print (array[i] + "\n");
        }
        System.out.println(array[1]);
        input.close();
        System.out.print("\nВыполняется поиск по хэшам...");
        Thread myThready0 = new Thread(new Runnable() 
		{
			public void run()  
			    {
                    String currentString = "e";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                    currentString = "f";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                    currentString = "g";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                    currentString = "h";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                } 
		});
		myThready0.start();	//Запуск потока
        Thread myThready1 = new Thread(new Runnable() 
		{
			public void run()  
			    {
                    String currentString = "i";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                    currentString = "j";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                    currentString = "k";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                    currentString = "l";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                } 
		});
        myThready1.start();	//Запуск потока
        Thread myThready2 = new Thread(new Runnable() 
		{
			public void run()  
			    {
                    String currentString = "m";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                    currentString = "n";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                    currentString = "o";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                    currentString = "p";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                } 
		});
		myThready2.start();	//Запуск потока
        Thread myThready3 = new Thread(new Runnable() 
		{
			public void run()  
			    {
                    String currentString = "q";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                    currentString = "r";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                    currentString = "s";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                    currentString = "t";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                } 
		});
        myThready3.start();	//Запуск потока
        Thread myThready4 = new Thread(new Runnable() 
		{
			public void run()  
			    {
                    String currentString = "u";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                    currentString = "v";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                    currentString = "w";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                    currentString = "x";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                } 
		});
		myThready4.start();	//Запуск потока
        Thread myThready5 = new Thread(new Runnable() 
		{
			public void run()  
			    {
                    String currentString = "y";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                    currentString = "z";
                    System.out.println(currentString);
                    try {
                        combineStringFromElements(currentString, 3);
                    } catch (NoSuchAlgorithmException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                } 
		});
        myThready5.start();	//Запуск потока
        System.out.println("a");
        combineStringFromElements("a", size);
        System.out.println("b");
        combineStringFromElements("b", size);
        System.out.println("c");
        combineStringFromElements("c", size);
        System.out.println("d");
        combineStringFromElements("d", size);
        System.out.println("Конец главного потока...");
    }   
    private static void combineStringFromElements(String currentString, int size) 
    throws NoSuchAlgorithmException, InterruptedException{
        String array[] = new String[4];
        array[0] = "1115dd800feaacefdf481f1f9070374a2a81e27880f187396db67958b207cbad";
        array[1] = "3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b";
        array[2] = "74e1bb62f8dabb8125a58852b63bdf6eaef667cb56ac7f7cdba6d7305c50a22f";
        String[] elements = {"a", "b", "c", "d", "e", "f", "g",
         "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", 
         "s", "t", "u", "v", "w", "x", "y", "z"};
        int maxLength = 5;
        String password;
        String hesh;
        if (currentString.length() == maxLength) {  
            password = currentString;      
            MessageDigest md = MessageDigest.getInstance("SHA-256");
            byte[]hashInBytes = md.digest(password.getBytes(StandardCharsets.UTF_8));
            StringBuilder sb = new StringBuilder();
        for (byte b : hashInBytes) {
            sb.append(String.format("%02x", b));
        }
        hesh = sb.toString();
        for (int i = 0; i < size; i++) {
            if (hesh.equals(array[i])) 
            {    
                System.out.println("\nPassword: " + password);
                System.out.println("HEX: " + sb.toString());
            }
        }

            return;
        }
        for (String element : elements) {
            combineStringFromElements(currentString + element, size);
        }       
    }

}