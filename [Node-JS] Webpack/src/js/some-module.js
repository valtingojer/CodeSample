export const anonymousArrowFunction = _ => _ == "alert" ? alert('Hello World from someFunction') : console.log('Hello World from someFunction');

export const asyncFunction = async () => {
    try {
        const response = await fetch('https://jsonplaceholder.typicode.com/todos/1');
        const data = await response.json();
        alert(`async response: ${data.title}`);
        console.log(`async response: ${data.title}`);
    } catch (error) {
        alert("Error: " + error);
    }
};

export const optionalChainingFunction = () => {
    const user = {
        name: 'John Doe',
        address: {
            street: '123 Main St',
            city: 'New York',
            state: 'NY'
        }
    };

    let ahah = user?.address?.street;

    alert(`Hello ${user?.name} from ${user?.address?.city} and telephone ${user?.phone?.number || ''}`);
    console.log(`Hello ${user?.name} from ${user?.address?.city} and telephone ${user?.phone?.number || ''}`)
};

export const destructuringFunction = () => {
    const user = {
        name: 'John Doe',
        address: {
            street: '123 Main St',
            city: 'New York',
            state: 'NY'
        }
    };

    const { name, address: { city } } = user;

    alert(`Hello ${name} from ${city}`);
    console.log(`Hello ${name} from ${city}`);
};

export const customClass = () => {
    class Person {
        constructor(name, age) {
            this.name = name;
            this.age += age;
        }

        name;
        age = 10;
    }

    const person = new Person('John Doe', 30);

    alert(`Hello ${person.name} from ${person.age}`);
    console.log(`Hello ${person.name} from ${person.age}`);
};

//anonymousArrowFunction();
try {
    anonymousArrowFunction("console");
    anonymousArrowFunction("alert");
} catch (error) {
    alert(error);
}

//asyncFunction();
try {
    asyncFunction();
} catch (error) {
    alert(error);
}

//optionalChainingFunction();
try {
    optionalChainingFunction();
}
catch (error) {
    alert(error);
}

//destructuringFunction();
try {
    destructuringFunction();
}
catch (error) {
    alert(error);
}

//customClass();
try {
    customClass();
}
catch (error) {
    alert(error);
}