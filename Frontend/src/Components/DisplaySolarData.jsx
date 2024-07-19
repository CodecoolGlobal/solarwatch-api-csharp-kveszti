import styled from "styled-components";

export default function DisplaySolarData({dataToDisplay, cityToDisplay, typeOfSunData, setIsDisplayed}){
    const date = new Date (dataToDisplay);
  
    const DivForData = styled.div`
    background-color: antiquewhite;
    border-radius: 10px;
    width: 300px;
    padding: 10px;`
    
    
    
    return(
        <>
            <DivForData>
                <h3>The requested data:</h3>
                <h4>City: {cityToDisplay}</h4>
                <h4>Date: {date.toLocaleDateString()}</h4>
                <h4>{typeOfSunData}: {date.toLocaleTimeString()}</h4>
            <button onClick={()=>setIsDisplayed(false)}>Go back</button>
            </DivForData>
        </>
    )
}