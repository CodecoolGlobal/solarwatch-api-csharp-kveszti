import React from 'react';

function CityDisplayContainer({city, setIsEditMode, handleCityDelete}) {
    return (
        <div className='cityDisplayCont'>
            <div className='cityDisplayTextContents'>
            <h3>{city.name}</h3>
            <div>{city.country}</div>
            </div>
            <div className='actionButtons'>
                <button className='deleteButton' onClick={() => handleCityDelete(city.id)}>🗑️</button>
                <button className='editButton' onClick={()=> setIsEditMode(()=> true)}>✏️</button>
            </div>
        </div>
    );
}

export default CityDisplayContainer;