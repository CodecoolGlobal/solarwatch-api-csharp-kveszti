import React from 'react';

function CityEditContainer({city, setIsEditMode, handleCityDelete}) {
    return (
        <div className='cityDisplayCont'>
            <form className='cityDisplayFormContents'>
                <label htmlFor='cityName'>Name: </label>
                <input id='cityName' type="text"></input>
                <label htmlFor='country'>Country: </label>
                <input id='contry' type="text"></input>
                <label htmlFor='state'>State: </label>
                <input id='state' type="text"></input>
                <label htmlFor='country'>Country: </label>
                <input id='contry' type="text"></input>
            </form>
            <div className='actionButtons'>
                <button className='deleteButton' onClick={() => handleCityDelete(city.id)}>🗑️</button>
                <button className='editButton' onClick={() => setIsEditMode(() => null)}>✔️</button>
            </div>
        </div>
    );
}

export default CityEditContainer;